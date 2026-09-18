namespace Naninovel
{
    /// <summary>
    /// Implantation is able to convert objects.
    /// </summary>
    public interface IConverter
    {
        /// <inheritdoc cref="Convert"/>
        object ConvertBlocking (object obj, string path);
        /// <summary>
        /// Converts the specified source object with specified resource path to the target object.
        /// </summary>
        /// <param name="obj">The source object to convert.</param>
        /// <param name="path">Full resource path of the converted object.</param>
        /// <returns>Resulting object converted to the target type.</returns>
        UniTask<object> Convert (object obj, string path);
    }

    /// <summary>
    /// Implantation is able to convert <typeparamref name="TSource"/> to <typeparamref name="TResult"/>.
    /// </summary>
    public interface IConverter<TSource, TResult> : IConverter
    {
        /// <inheritdoc cref="IConverter.Convert"/>
        TResult ConvertBlocking (TSource obj, string path);
        /// <inheritdoc cref="IConverter.Convert"/>
        UniTask<TResult> Convert (TSource obj, string path);
    }
}
