
namespace LeaRun.Application.Code
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：LR01010
    /// 日 期：2015.10.10
    /// 描 述：当前操作者回话接口
    /// </summary>
    public interface OperatorIProvider
    {
        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        /// <param name="dataAuthorize">数据权限信息</param>
        void AddCurrent(Operator user, AuthorizeDataModel dataAuthorize);
        /// <summary>
        /// 跟新登录信息缓存
        /// </summary>
        /// <param name="user"></param>
        void UpdateCurrent(Operator user);
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        Operator Current();
        /// <summary>
        /// 获取当前用户的数据权限
        /// </summary>
        /// <returns></returns>
        AuthorizeDataModel CurrentDataAuthorize();
        /// <summary>
        /// 设置当前的数据权限
        /// </summary>
        /// <param name="model"></param>
        void AddCurrentDataAuthorize(AuthorizeDataModel dataAuthorize, string userId);
        /// <summary>
        /// 删除当前用户
        /// </summary>
        void EmptyCurrent();
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        bool IsOverdue();
        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        int IsOnLine();
    }
}
