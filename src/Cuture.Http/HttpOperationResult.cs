﻿using System;
using System.Net.Http;

namespace Cuture.Http
{
    /// <summary>
    /// http请求结果
    /// </summary>
    public class HttpOperationResult
    {
        #region 属性

        /// <summary>
        /// 执行过程出现的异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 获取一个值，该值指示 HTTP 请求是否成功
        /// </summary>
        public bool IsSuccessStatusCode => ResponseMessage is null ? false : ResponseMessage.IsSuccessStatusCode;

        /// <summary>
        /// 原始响应信息
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; set; }

        #endregion 属性

        #region 方法

        /// <summary>
        /// 获取请求返回头的 Set-Cookie 字符串内容
        /// </summary>
        /// <returns></returns>
        public string GetCookie() => ResponseMessage.GetCookie();

        #endregion 方法
    }

    /// <summary>
    /// http请求结果
    /// </summary>
    /// <typeparam name="T">内容类型</typeparam>
    public class HttpOperationResult<T> : HttpOperationResult
    {
        #region 属性

        /// <summary>
        /// 内容
        /// </summary>
        public T Data { get; set; }

        #endregion 属性

        #region 构造函数

        /// <summary>
        /// http请求结果
        /// </summary>
        public HttpOperationResult()
        {
        }

        /// <summary>
        /// http请求结果
        /// </summary>
        /// <param name="responseMessage">原始响应信息</param>
        /// <param name="data">内容</param>
        public HttpOperationResult(HttpResponseMessage responseMessage, T data)
        {
            ResponseMessage = responseMessage;
            Data = data;
        }

        #endregion 构造函数
    }

    /// <summary>
    /// 响应正文为文本的http请求结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TextHttpOperationResult<T> : HttpOperationResult<T>
    {
        #region 属性

        /// <summary>
        /// 响应正文文本
        /// </summary>
        public string Text { get; set; }

        #endregion 属性

        #region 构造函数

        /// <summary>
        /// 响应正文为文本的http请求结果
        /// </summary>
        public TextHttpOperationResult()
        {
        }

        /// <summary>
        /// 响应正文为文本的http请求结果
        /// </summary>
        /// <param name="responseMessage">原始响应信息</param>
        /// <param name="text">响应正文文本</param>
        public TextHttpOperationResult(HttpResponseMessage responseMessage, string text)
        {
            ResponseMessage = responseMessage;
            Text = text;
        }

        /// <summary>
        /// 响应正文为文本的http请求结果
        /// </summary>
        /// <param name="responseMessage">原始响应信息</param>
        /// <param name="data"></param>
        public TextHttpOperationResult(HttpResponseMessage responseMessage, T data)
        {
            ResponseMessage = responseMessage;
            Data = data;
        }

        #endregion 构造函数
    }
}