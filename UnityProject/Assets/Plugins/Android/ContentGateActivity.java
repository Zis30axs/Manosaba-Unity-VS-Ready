// Launcher gate of the Android player.  The Addressables bundles (about 4.3 GB) cannot live in the APK or in the OBB (zip offsets
// are 32-bit), so they ship as a few plain zip parts next to the APK.  This activity runs before Unity: when the bundles listed in
// assets/manosaba-content.json are all present in <external files dir>/aa/Android/Android it starts the game at once, otherwise it
// lets the player pick the zip parts with the system document picker (no storage permission needed, works on Android 10 - 16) and
// streams their bundles into that folder.  Written by tools/unity-rebuild/package_android_content.py's counterpart in the editor
// (AndroidContentGate); the Unity-side AndroidAddressablesSidecar then maps every bundle location to these files.
package com.acacia.manosaba.gate;

import android.app.Activity;
import android.content.ClipData;
import android.content.Intent;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import android.os.StatFs;
import android.util.TypedValue;
import android.view.Gravity;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.BufferedReader;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;

public class ContentGateActivity extends Activity {
    private static final int REQUEST_PICK = 4711;
    private static final String MANIFEST_ASSET = "manosaba-content.json";
    private static final String GAME_ACTIVITY = "com.unity3d.player.UnityPlayerGameActivity";

    private final Map<String, Long> expected = new LinkedHashMap<String, Long>();
    private String buildId = "";
    private File contentDir;
    private boolean zh;

    private TextView statusView;
    private TextView detailView;
    private ProgressBar bar;
    private Button pickButton;
    private Button startButton;
    private volatile boolean importing;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        zh = Locale.getDefault().getLanguage().startsWith("zh");
        File external = getExternalFilesDir(null);
        if (external == null || !loadManifest()) {
            // No manifest means a build without the sidecar content, no external storage means nothing this screen could do.
            startGame();
            return;
        }
        contentDir = new File(external, "aa/Android/Android");
        if (missing().isEmpty()) {
            startGame();
            return;
        }
        buildUi();
        refresh(null);
    }

    private boolean loadManifest() {
        try (InputStream in = getAssets().open(MANIFEST_ASSET);
             BufferedReader reader = new BufferedReader(new InputStreamReader(in, "UTF-8"))) {
            StringBuilder text = new StringBuilder();
            String line;
            while ((line = reader.readLine()) != null) text.append(line).append('\n');
            JSONObject json = new JSONObject(text.toString());
            buildId = json.optString("buildId", "");
            JSONArray files = json.getJSONArray("files");
            for (int i = 0; i < files.length(); i++) {
                JSONObject file = files.getJSONObject(i);
                expected.put(file.getString("n"), file.getLong("s"));
            }
            return !expected.isEmpty();
        } catch (Exception exception) {
            return false;
        }
    }

    private List<String> missing() {
        List<String> names = new ArrayList<String>();
        for (Map.Entry<String, Long> entry : expected.entrySet()) {
            File file = new File(contentDir, entry.getKey());
            if (!file.isFile() || file.length() != entry.getValue()) names.add(entry.getKey());
        }
        return names;
    }

    private long bytesOf(List<String> names) {
        long total = 0;
        for (String name : names) total += expected.get(name);
        return total;
    }

    private String s(String chinese, String english) {
        return zh ? chinese : english;
    }

    private int dp(int value) {
        return (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, value, getResources().getDisplayMetrics());
    }

    private void buildUi() {
        LinearLayout root = new LinearLayout(this);
        root.setOrientation(LinearLayout.VERTICAL);
        root.setGravity(Gravity.CENTER);
        root.setBackgroundColor(Color.rgb(22, 18, 20));
        root.setPadding(dp(48), dp(24), dp(48), dp(24));

        TextView title = new TextView(this);
        title.setText(s("魔法少女的魔女审判 - 导入游戏资源", "Manosaba - import game data"));
        title.setTextColor(Color.WHITE);
        title.setTextSize(TypedValue.COMPLEX_UNIT_SP, 22);
        title.setGravity(Gravity.CENTER);
        root.addView(title);

        statusView = new TextView(this);
        statusView.setTextColor(Color.rgb(230, 220, 224));
        statusView.setTextSize(TypedValue.COMPLEX_UNIT_SP, 16);
        statusView.setGravity(Gravity.CENTER);
        statusView.setPadding(0, dp(18), 0, dp(6));
        root.addView(statusView);

        detailView = new TextView(this);
        detailView.setTextColor(Color.rgb(160, 150, 156));
        detailView.setTextSize(TypedValue.COMPLEX_UNIT_SP, 13);
        detailView.setGravity(Gravity.CENTER);
        root.addView(detailView);

        bar = new ProgressBar(this, null, android.R.attr.progressBarStyleHorizontal);
        bar.setMax(1000);
        LinearLayout.LayoutParams barParams = new LinearLayout.LayoutParams(dp(520), dp(12));
        barParams.setMargins(0, dp(18), 0, dp(18));
        root.addView(bar, barParams);

        pickButton = new Button(this);
        pickButton.setText(s("选择资源包（.zip，可多选）", "Choose data packs (.zip, several allowed)"));
        pickButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                pick();
            }
        });
        root.addView(pickButton);

        startButton = new Button(this);
        startButton.setText(s("开始游戏", "Start game"));
        startButton.setVisibility(View.GONE);
        startButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startGame();
            }
        });
        root.addView(startButton);

        setContentView(root);
    }

    private void pick() {
        if (importing) return;
        Intent intent = new Intent(Intent.ACTION_OPEN_DOCUMENT);
        intent.addCategory(Intent.CATEGORY_OPENABLE);
        intent.setType("*/*");
        intent.putExtra(Intent.EXTRA_MIME_TYPES, new String[]{"application/zip", "application/x-zip-compressed", "application/octet-stream"});
        intent.putExtra(Intent.EXTRA_ALLOW_MULTIPLE, true);
        startActivityForResult(intent, REQUEST_PICK);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode != REQUEST_PICK || resultCode != RESULT_OK || data == null || importing) return;
        final List<Uri> uris = new ArrayList<Uri>();
        ClipData clip = data.getClipData();
        if (clip != null) {
            for (int i = 0; i < clip.getItemCount(); i++) uris.add(clip.getItemAt(i).getUri());
        } else if (data.getData() != null) {
            uris.add(data.getData());
        }
        if (uris.isEmpty()) return;

        List<String> missing = missing();
        long needed = bytesOf(missing);
        long free = new StatFs(getExternalFilesDir(null).getPath()).getAvailableBytes();
        if (free < needed + (200L << 20)) {
            refresh(s("存储空间不足：还需要 ", "Not enough free storage: need ") + gb(needed) + s("，可用 ", ", free ") + gb(free));
            return;
        }
        importing = true;
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        pickButton.setEnabled(false);
        final long total = needed;
        new Thread(new Runnable() {
            @Override
            public void run() {
                importAll(uris, total);
            }
        }, "ManosabaContentImport").start();
    }

    private void importAll(List<Uri> uris, long total) {
        String error = null;
        long done = 0;
        long lastUi = 0;
        long startedAt = System.currentTimeMillis();
        byte[] buffer = new byte[1 << 20];
        File[] stale = contentDir.listFiles();
        if (stale != null) for (File file : stale) if (file.getName().endsWith(".part")) file.delete();
        if (!contentDir.isDirectory() && !contentDir.mkdirs()) error = s("无法创建目录 ", "Cannot create ") + contentDir;
        for (int index = 0; index < uris.size() && error == null; index++) {
            Uri uri = uris.get(index);
            try (InputStream raw = getContentResolver().openInputStream(uri);
                 ZipInputStream zip = new ZipInputStream(new BufferedInputStream(raw, 1 << 20))) {
                ZipEntry entry;
                boolean any = false;
                while ((entry = zip.getNextEntry()) != null) {
                    any = true;
                    if (entry.isDirectory()) continue;
                    String name = new File(entry.getName()).getName();
                    Long size = expected.get(name);
                    if (size == null) continue;                                 // not one of this build's bundles
                    File target = new File(contentDir, name);
                    if (target.isFile() && target.length() == size) continue;   // already imported
                    File part = new File(contentDir, name + ".part");
                    long written = 0;
                    try (FileOutputStream out = new FileOutputStream(part)) {
                        int read;
                        while ((read = zip.read(buffer)) > 0) {
                            out.write(buffer, 0, read);
                            written += read;
                            done += read;
                            long now = System.currentTimeMillis();
                            if (now - lastUi > 250) {
                                lastUi = now;
                                progress(done, total, startedAt, name);
                            }
                        }
                    }
                    if (written != size) {
                        part.delete();
                        throw new java.io.IOException(name + ": " + written + " of " + size + " bytes");
                    }
                    if (!part.renameTo(target)) {
                        part.delete();
                        throw new java.io.IOException(s("无法写入 ", "Cannot write ") + target);
                    }
                }
                if (!any) error = s("不是有效的资源包：", "Not a data pack: ") + uri.getLastPathSegment();
            } catch (Exception exception) {
                error = exception.getClass().getSimpleName() + ": " + exception.getMessage();
            }
        }
        final String finalError = error;
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                importing = false;
                getWindow().clearFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
                pickButton.setEnabled(true);
                refresh(finalError);
                if (finalError == null && missing().isEmpty()) startGame();
            }
        });
    }

    private void progress(final long done, final long total, final long startedAt, final String name) {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                double seconds = Math.max(1, System.currentTimeMillis() - startedAt) / 1000.0;
                statusView.setText(s("正在导入 ", "Importing ") + gb(done) + " / " + gb(total));
                detailView.setText(name + "\n" + String.format(Locale.US, "%.0f MB/s", done / 1048576.0 / seconds));
                bar.setProgress((int) (total == 0 ? 0 : done * 1000 / total));
            }
        });
    }

    private void refresh(String error) {
        List<String> missing = missing();
        if (missing.isEmpty()) {
            statusView.setText(s("资源已齐全", "All data present"));
            detailView.setText("");
            bar.setProgress(1000);
            pickButton.setVisibility(View.GONE);
            startButton.setVisibility(View.VISIBLE);
            return;
        }
        long total = 0;
        for (long size : expected.values()) total += size;
        long have = total - bytesOf(missing);
        bar.setProgress((int) (have * 1000 / Math.max(1, total)));
        statusView.setText(error != null ? error
                : s("缺少 ", "Missing ") + missing.size() + s(" 个资源文件（", " files (") + gb(bytesOf(missing)) + s("）。请选择资源包 zip（可一次选多个）。",
                "). Choose the data pack zips (several at once)."));
        detailView.setText(s("资源版本 ", "Data version ") + buildId + (error != null ? "\n" + s("还缺 ", "Still missing ") + missing.size() : ""));
    }

    private static String gb(long bytes) {
        return String.format(Locale.US, "%.2f GB", bytes / 1073741824.0);
    }

    private void startGame() {
        Intent intent = new Intent();
        intent.setClassName(this, GAME_ACTIVITY);
        startActivity(intent);
        finish();
    }
}
