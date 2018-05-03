using System.Collections.Generic;

namespace EduHub.Models.UsersControllerModels
{
    public class SearchOfUserRequest
    {
        /// <summary>
        ///     username fragment
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        ///     Flag value
        /// </summary>
        public bool WantToTeach { get; set; } = false;

        /// <summary>
        ///     Teacher Skills
        /// </summary>
        public List<string> Skills { get; set; } = new List<string>();

        /// <summary>
        ///     Teacher Experience (number of classes)
        /// </summary>
        public TeacherExperience TeacherExperience { get; set; }

        /// <summary>
        ///     User Experience (number of classes)
        /// </summary>
        public UserExperience UserExperience { get; set; }
    }
}