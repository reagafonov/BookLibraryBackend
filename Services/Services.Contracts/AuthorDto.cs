namespace Services.Contracts
{
    /// <summary>
    /// ДТО автора
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия автора
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string FirstName { get; set; }
    }
}