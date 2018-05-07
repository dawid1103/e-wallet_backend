namespace EwalletCommon.Models
{
    public class CategoryDTO
    {
        /// <summary>
        /// Category id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// RGBA color format
        /// </summary>
        public string Color { get; set; } = "FFFFFF";

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }
    }
}
