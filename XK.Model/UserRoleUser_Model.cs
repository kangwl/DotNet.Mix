namespace XK.Model {
    /// <summary>
    /// 用户和角色对应表
    /// </summary>
    public class UserRoleUser_Model {
        /// <summary>
        /// 主键 自增
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户表ID 主键
        /// </summary>
        public int Users_ID { get; set; }
        /// <summary>
        /// 用户角色ID UserRole_Model 的 ID
        /// </summary>
        public int UserRole_ID { get; set; }

    }
}
