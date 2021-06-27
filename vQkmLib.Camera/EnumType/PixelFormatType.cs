namespace vQkmLib.Camera
{
    /// <summary>
    /// 像素类型
    /// </summary>
    public enum PixelFormatType : long
    {
        /// <summary>
        /// GVSP_PIX_MONO8
        /// </summary>
        GVSP_PIX_MONO8 = 17301505,

        /// <summary>
        /// GVSP_PIX_MONO8_SIGNED
        /// </summary>
        GVSP_PIX_MONO8_SIGNED = 17301506,

        /// <summary>
        /// GVSP_PIX_BAYGR8
        /// </summary>
        GVSP_PIX_BAYGR8 = 17301512,

        /// <summary>
        /// GVSP_PIX_BAYRG8
        /// </summary>
        GVSP_PIX_BAYRG8 = 17301513,

        /// <summary>
        /// GVSP_PIX_BAYGB8
        /// </summary>
        GVSP_PIX_BAYGB8 = 17301514,

        /// <summary>
        /// GVSP_PIX_BAYBG8
        /// </summary>
        GVSP_PIX_BAYBG8 = 17301515,

        /// <summary>
        /// PFNC_PIX_MONO10_PACKED
        /// </summary>
        PFNC_PIX_MONO10_PACKED = 17432646,

        /// <summary>
        /// PFNC_PIX_BAYBG10_PACKED
        /// </summary>
        PFNC_PIX_BAYBG10_PACKED = 17432658,

        /// <summary>
        /// PFNC_PIX_BAYGB10_PACKED
        /// </summary>
        PFNC_PIX_BAYGB10_PACKED = 17432660,

        /// <summary>
        /// PFNC_PIX_BAYGR10_PACKED
        /// </summary>
        PFNC_PIX_BAYGR10_PACKED = 17432662,

        /// <summary>
        /// PFNC_PIX_BAYRG10_PACKED
        /// </summary>
        PFNC_PIX_BAYRG10_PACKED = 17432664,

        /// <summary>
        /// GVSP_PIX_MONO10_PACKED
        /// </summary>
        GVSP_PIX_MONO10_PACKED = 17563652,

        /// <summary>
        /// GVSP_PIX_MONO12_PACKED
        /// </summary>
        GVSP_PIX_MONO12_PACKED = 17563654,

        /// <summary>
        /// GVSP_PIX_BAYGR10_PACKED
        /// </summary>
        GVSP_PIX_BAYGR10_PACKED = 17563686,

        /// <summary>
        /// GVSP_PIX_BAYRG10_PACKED
        /// </summary>
        GVSP_PIX_BAYRG10_PACKED = 17563687,

        /// <summary>
        /// GVSP_PIX_BAYGB10_PACKED
        /// </summary>
        GVSP_PIX_BAYGB10_PACKED = 17563688,

        /// <summary>
        /// GVSP_PIX_BAYBG10_PACKED
        /// </summary>
        GVSP_PIX_BAYBG10_PACKED = 17563689,

        /// <summary>
        /// GVSP_PIX_BAYGR12_PACKED
        /// </summary>
        GVSP_PIX_BAYGR12_PACKED = 17563690,

        /// <summary>
        /// GVSP_PIX_BAYRG12_PACKED
        /// </summary>
        GVSP_PIX_BAYRG12_PACKED = 17563691,

        /// <summary>
        /// GVSP_PIX_BAYGB12_PACKED
        /// </summary>
        GVSP_PIX_BAYGB12_PACKED = 17563692,

        /// <summary>
        /// GVSP_PIX_BAYBG12_PACKED
        /// </summary>
        GVSP_PIX_BAYBG12_PACKED = 17563693,

        /// <summary>
        /// PFNC_PIX_MONO12_PACKED
        /// </summary>
        PFNC_PIX_MONO12_PACKED = 17563719,

        /// <summary>
        /// PFNC_PIX_BAYBG12_PACKED
        /// </summary>
        PFNC_PIX_BAYBG12_PACKED = 17563731,

        /// <summary>
        /// PFNC_PIX_BAYGB12_PACKED
        /// </summary>
        PFNC_PIX_BAYGB12_PACKED = 17563733,

        /// <summary>
        /// PFNC_PIX_BAYGR12_PACKED
        /// </summary>
        PFNC_PIX_BAYGR12_PACKED = 17563735,

        /// <summary>
        /// PFNC_PIX_BAYRG12_PACKED
        /// </summary>
        PFNC_PIX_BAYRG12_PACKED = 17563737,

        /// <summary>
        /// GVSP_PIX_MONO10
        /// </summary>
        GVSP_PIX_MONO10 = 17825795,

        /// <summary>
        /// GVSP_PIX_MONO12
        /// </summary>
        GVSP_PIX_MONO12 = 17825797,

        /// <summary>
        /// GVSP_PIX_MONO16
        /// </summary>
        GVSP_PIX_MONO16 = 17825799,

        /// <summary>
        /// GVSP_PIX_BAYGR10
        /// </summary>
        GVSP_PIX_BAYGR10 = 17825804,

        /// <summary>
        /// GVSP_PIX_BAYRG10
        /// </summary>
        GVSP_PIX_BAYRG10 = 17825805,

        /// <summary>
        /// GVSP_PIX_BAYGB10
        /// </summary>
        GVSP_PIX_BAYGB10 = 17825806,

        /// <summary>
        /// GVSP_PIX_BAYBG10
        /// </summary>
        GVSP_PIX_BAYBG10 = 17825807,

        /// <summary>
        /// GVSP_PIX_BAYGR12
        /// </summary>
        GVSP_PIX_BAYGR12 = 17825808,

        /// <summary>
        /// GVSP_PIX_BAYRG12
        /// </summary>
        GVSP_PIX_BAYRG12 = 17825809,

        /// <summary>
        /// GVSP_PIX_BAYGB12
        /// </summary>
        GVSP_PIX_BAYGB12 = 17825810,

        /// <summary>
        /// GVSP_PIX_BAYBG12
        /// </summary>
        GVSP_PIX_BAYBG12 = 17825811,

        /// <summary>
        /// GVSP_PIX_MONO14
        /// </summary>
        GVSP_PIX_MONO14 = 17825829,

        /// <summary>
        /// GVSP_PIX_BAYGR16
        /// </summary>
        GVSP_PIX_BAYGR16 = 17825838,

        /// <summary>
        /// GVSP_PIX_BAYRG16
        /// </summary>
        GVSP_PIX_BAYRG16 = 17825839,

        /// <summary>
        /// GVSP_PIX_BAYGB16
        /// </summary>
        GVSP_PIX_BAYGB16 = 17825840,

        /// <summary>
        /// GVSP_PIX_BAYBG16
        /// </summary>
        GVSP_PIX_BAYBG16 = 17825841,

        /// <summary>
        /// GVSP_PIX_YUV411_PACKED
        /// </summary>
        GVSP_PIX_YUV411_PACKED = 34340894,

        /// <summary>
        /// GVSP_PIX_YUV422_PACKED
        /// </summary>
        GVSP_PIX_YUV422_PACKED = 34603039,

        /// <summary>
        /// GVSP_PIX_YUV422_YUYV_PACKED
        /// </summary>
        GVSP_PIX_YUV422_YUYV_PACKED = 34603058,

        /// <summary>
        /// GVSP_PIX_RGB8_PACKED
        /// </summary>
        GVSP_PIX_RGB8_PACKED = 35127316,

        /// <summary>
        /// GVSP_PIX_BGR8_PACKED
        /// </summary>
        GVSP_PIX_BGR8_PACKED = 35127317,

        /// <summary>
        /// GVSP_PIX_YUV444_PACKED
        /// </summary>
        GVSP_PIX_YUV444_PACKED = 35127328,

        /// <summary>
        /// GVSP_PIX_RGB8_PLANAR
        /// </summary>
        GVSP_PIX_RGB8_PLANAR = 35127329,

        /// <summary>
        /// PFNC_PIX_BGR10_P
        /// </summary>
        PFNC_PIX_BGR10_P = 35520584,

        /// <summary>
        /// PFNC_PIX_RGB10_P
        /// </summary>
        PFNC_PIX_RGB10_P = 35520604,

        /// <summary>
        /// GVSP_PIX_RGBA8_PACKED
        /// </summary>
        GVSP_PIX_RGBA8_PACKED = 35651606,

        /// <summary>
        /// GVSP_PIX_BGRA8_PACKED
        /// </summary>
        GVSP_PIX_BGRA8_PACKED = 35651607,

        /// <summary>
        /// GVSP_PIX_RGB10V1_PACKED
        /// </summary>
        GVSP_PIX_RGB10V1_PACKED = 35651612,

        /// <summary>
        /// GVSP_PIX_RGB10V2_PACKED
        /// </summary>
        GVSP_PIX_RGB10V2_PACKED = 35651613,

        /// <summary>
        /// GVSP_PIX_RGB8A
        /// </summary>
        GVSP_PIX_RGB8A = 35651621,

        /// <summary>
        /// GVSP_PIX_RGB12V1_PACKED
        /// </summary>
        GVSP_PIX_RGB12V1_PACKED = 35913780,

        /// <summary>
        /// PFNC_PIX_BGR12_P
        /// </summary>
        PFNC_PIX_BGR12_P = 35913801,

        /// <summary>
        /// PFNC_PIX_RGB12_P
        /// </summary>
        PFNC_PIX_RGB12_P = 35913821,

        /// <summary>
        /// PFNC_PIX_BGRA10_PACKED
        /// </summary>
        PFNC_PIX_BGRA10_PACKED = 36175949,

        /// <summary>
        /// PFNC_PIX_RGBA10_PACKED
        /// </summary>
        PFNC_PIX_RGBA10_PACKED = 36175968,

        /// <summary>
        /// GVSP_PIX_BGR16_PACKED_INTERNAL
        /// </summary>
        GVSP_PIX_BGR16_PACKED_INTERNAL = 36700160,

        /// <summary>
        /// GVSP_PIX_RGB10_PACKED
        /// </summary>
        GVSP_PIX_RGB10_PACKED = 36700184,

        /// <summary>
        /// GVSP_PIX_BGR10_PACKED
        /// </summary>
        GVSP_PIX_BGR10_PACKED = 36700185,

        /// <summary>
        /// GVSP_PIX_RGB12_PACKED
        /// </summary>
        GVSP_PIX_RGB12_PACKED = 36700186,

        /// <summary>
        /// GVSP_PIX_BGR12_PACKED
        /// </summary>
        GVSP_PIX_BGR12_PACKED = 36700187,

        /// <summary>
        /// GVSP_PIX_RGB10_PLANAR
        /// </summary>
        GVSP_PIX_RGB10_PLANAR = 36700194,

        /// <summary>
        /// GVSP_PIX_RGB12_PLANAR
        /// </summary>
        GVSP_PIX_RGB12_PLANAR = 36700195,

        /// <summary>
        /// GVSP_PIX_RGB16_PLANAR
        /// </summary>
        GVSP_PIX_RGB16_PLANAR = 36700196,

        /// <summary>
        /// GVSP_PIX_RGB16_PACKED
        /// </summary>
        GVSP_PIX_RGB16_PACKED = 36700211,

        /// <summary>
        /// PFNC_PIX_BGRA12_PACKED
        /// </summary>
        PFNC_PIX_BGRA12_PACKED = 36700239,

        /// <summary>
        /// PFNC_PIX_RGBA12_PACKED
        /// </summary>
        PFNC_PIX_RGBA12_PACKED = 36700258,

        /// <summary>
        /// PFNC_PIX_BGRA10
        /// </summary>
        PFNC_PIX_BGRA10 = 37748812,

        /// <summary>
        /// PFNC_PIX_BGRA12
        /// </summary>
        PFNC_PIX_BGRA12 = 37748814,

        /// <summary>
        /// PFNC_PIX_BGRA14
        /// </summary>
        PFNC_PIX_BGRA14 = 37748816,

        /// <summary>
        /// PFNC_PIX_BGRA16
        /// </summary>
        PFNC_PIX_BGRA16 = 37748817,

        /// <summary>
        /// PFNC_PIX_RGBA10
        /// </summary>
        PFNC_PIX_RGBA10 = 37748831,

        /// <summary>
        /// PFNC_PIX_RGBA12
        /// </summary>
        PFNC_PIX_RGBA12 = 37748833,

        /// <summary>
        /// PFNC_PIX_RGBA14
        /// </summary>
        PFNC_PIX_RGBA14 = 37748835,

        /// <summary>
        /// PFNC_PIX_RGBA16
        /// </summary>
        PFNC_PIX_RGBA16 = 37748836,

        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 9999999999
    }

}
