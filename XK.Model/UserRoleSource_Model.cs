namespace XK.Model {
    public class UserRoleMenu_Model {
        /// <summary>
        /// 主键 自增长
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户角色ID
        /// </summary>
        public int UserRole_ID { get; set; }
        /// <summary>
        /// 资源名
        /// </summary>
        public string SourceName { get; set; }
        //list delete update detail
        public int act { get; set; }
    }
}
