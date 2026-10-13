// Procedural compatibility approximation; this is not reconstructed Shader Graph source.
float TitleHash(float2 p)
{
    p = frac(p * float2(123.34, 456.21));
    p += dot(p, p + 45.32);
    return frac(p.x * p.y);
}

float TitleNoise(float2 p)
{
    float2 i = floor(p), f = frac(p);
    float2 u = f * f * (3.0 - 2.0 * f);
    return lerp(lerp(TitleHash(i), TitleHash(i + float2(1,0)), u.x),
                lerp(TitleHash(i + float2(0,1)), TitleHash(i + float2(1,1)), u.x), u.y);
}

float TitleCloud(float2 uv)
{
    return TitleNoise(uv) * 0.57 + TitleNoise(uv * 2.03) * 0.28 + TitleNoise(uv * 4.11) * 0.15;
}
