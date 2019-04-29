using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIM.DocTransition
{
    /// <summary>
    /// 转码源文件格式
    /// </summary>
    public enum NIMDocTranscodingFileType
    {
        /// <summary>
        ///ppt 
        /// </summary>
        kNIMDocTranscodingFileTypePPT = 1,
        /// <summary>
        /// pptx
        /// </summary>
        kNIMDocTranscodingFileTypePPTX = 2,
        /// <summary>
        /// pdf
        /// </summary>
        kNIMDocTranscodingFileTypePDF = 3
    }

    /// <summary>
    /// 转码目标图像文件类型
    /// </summary>
    public enum NIMDocTranscodingImageType
    {
        /// <summary>
        ///转码为 jpg 图片 
        /// </summary>
        kNIMDocTranscodingImageTypeJPG = 10,
        /// <summary>
        /// 转码为 png 图片
        /// </summary>
        kNIMDocTranscodingImageTypePNG = 11
    }

    /// <summary>
    /// 转码图像清晰度
    /// </summary>
    public enum NIMDocTranscodingQuality
    {
        /// <summary>
        ///高清转码质量 
        /// </summary>
        kNIMDocTranscodingQualityHigh = 1,
        /// <summary>
        ///中等转码质量 
        /// </summary>
        kNIMDocTranscodingQualityMedium = 2,
        /// <summary>
        ///低清转码质量 
        /// </summary>
        kNIMDocTranscodingQualityLow = 3
    }

    /// <summary>
    /// 续传过程状态
    /// </summary>
    public enum NIMDocContinueUploadState
    {
        /// <summary>
        /// 没有进行过上传
        /// </summary>
        kNIMDocContinueUploadNone = 0,
        /// <summary>
        /// 文件续传中
        /// </summary>
        kNIMDocContinueUploading = 1,
        /// <summary>
        /// 文件续传失败
        /// </summary>
        kNIMDocContinueUploadFailed = 2,
        /// <summary>
        /// 文件续传完成
        /// </summary>
        kNIMDocContinueUploadCompleted = 3
    }

    /// <summary>
    /// 转码过程状态
    /// </summary>
    public enum NIMDocTranscodingState
    {
        /// <summary>
        /// 转码装备中
        /// </summary>
        kNIMDocTranscodingStatePreparing = 1,
        /// <summary>
        /// 转码进行中
        /// </summary>
        kNIMDocTranscodingStateOngoing = 2,
        /// <summary>
        /// 转码超时
        /// </summary>
        kNIMDocTranscodingStateTimeout = 3,
        /// <summary>
        /// 转码完成
        /// </summary>
        kNIMDocTranscodingStateCompleted = 4,
        /// <summary>
        /// 转码失败
        /// </summary>
        kNIMDocTranscodingStateFailed = 5
    }

    /// <summary>
    /// 转码失败原因
    /// </summary>
    public enum NIMDocTranscodingFailFlag
    {
        /// <summary>
        /// 正常
        /// </summary>
        kNIMDocTransFailFlagSuccess = 0,
        /// <summary>
        /// 找不到文件
        /// </summary>
        kNIMDocTransFailFlagNotExist = 2,
        /// <summary>
        /// 文件类型错误 
        /// </summary>
        kNIMDocTransFailFlagFileTypeErr = 3,
        /// <summary>
        /// 转码请求出现异常
        /// </summary>
        kNIMDocTransFailFlagRequstErr = 4,
        /// <summary>
        /// 转码服务器连接错误
        /// </summary>
        kNIMDocTransFailFlagLinkErr = 5,
        /// <summary>
        /// 转码服务器内部错误
        /// </summary>
        kNIMDocTransFailFlagServerErr = 6,
        /// <summary>
        /// 文档转码图片出错
        /// </summary>
        kNIMDocTransFailFlagPicErr = 7,
        /// <summary>
        /// 图片质量处理错误
        /// </summary>
        kNIMDocTransFailFlagQualityErr = 8,
        /// <summary>
        /// 页数超限
        /// </summary>
        kNIMDocTransFailFlagPageLimit = 9,
        /// <summary>
        /// nos回调错误
        /// </summary>
        kNIMDocTransFailFlagNosErr = 10,
        /// <summary>
        /// 文档解析错误
        /// </summary>
        kNIMDocTransFailFlagDocParseErr = 11,
        /// <summary>
        /// 表示未知错误
        /// </summary>
        kNIMDocTransFailFlagUnknown = 100
    }
}
