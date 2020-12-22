﻿using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cuture.Http
{
    /// <summary>
    /// HttpFormContent
    /// </summary>
    public class FormContent : ByteArrayContent
    {
        #region 字段

        /// <summary>
        /// <see cref="FormContent"/> 的默认ContentType
        /// </summary>
        public const string ContentType = "application/x-www-form-urlencoded";

        /// <summary>
        /// 空的Content
        /// </summary>
        private static readonly byte[] EmptyContent = Array.Empty<byte>();

        #endregion 字段

        #region 构造函数

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">已编码的from内容</param>
        public FormContent(string content) : this(content, ContentType, Encoding.UTF8)
        {
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">已编码的from内容</param>
        /// <param name="encoding">指定编码类型</param>
        public FormContent(string content, Encoding encoding) : this(content, ContentType, encoding)
        {
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">已编码的from内容</param>
        /// <param name="contentType">指定ContentType</param>
        public FormContent(string content, string contentType) : this(content, contentType, Encoding.UTF8)
        {
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">已编码的from内容</param>
        /// <param name="contentType">指定ContentType</param>
        /// <param name="encoding">指定编码类型</param>
        public FormContent(string content, string contentType, Encoding encoding) : base(GetBytes(content, encoding))
        {
            Headers.TryAddWithoutValidation(HttpHeaderDefinitions.ContentType, contentType);
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">用于转换为form的对象</param>
        public FormContent(object content) : this(content, ContentType, Encoding.UTF8)
        {
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">用于转换为form的对象</param>
        /// <param name="encoding">指定编码类型</param>
        public FormContent(object content, Encoding encoding) : this(content, ContentType, encoding)
        {
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">用于转换为form的对象</param>
        /// <param name="contentType">指定ContentType</param>
        public FormContent(object content, string contentType) : this(content, contentType, Encoding.UTF8)
        {
        }

        /// <summary>
        /// HttpFormContent
        /// </summary>
        /// <param name="content">用于转换为form的对象</param>
        /// <param name="contentType">指定ContentType</param>
        /// <param name="encoding">指定编码类型</param>
        public FormContent(object content, string contentType, Encoding encoding) : base(GetBytes(content, encoding))
        {
            Headers.TryAddWithoutValidation(HttpHeaderDefinitions.ContentType, contentType);
        }

        #endregion 构造函数

        #region 方法

        private static byte[] GetBytes(object content, Encoding encoding)
        {
            if (content is null)
            {
                return EmptyContent;
            }
            else if (content is string str)
            {
                return GetBytes(str, encoding);
            }

            var data = string.Empty;

            if (string.IsNullOrEmpty(data))
            {
                data = content.ToEncodedForm();
            }

            return encoding.GetBytes(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] GetBytes(string data, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data))
            {
                return EmptyContent;
            }
            return encoding.GetBytes(
                    data
#if NET5_0
                        .Replace("%20", "+", StringComparison.Ordinal)
#else
                        .Replace("%20", "+")
#endif
                        );
        }

        #endregion 方法
    }
}