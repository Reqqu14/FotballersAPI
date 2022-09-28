using System.ComponentModel;

namespace FotballersAPI.Domain.Enums
{
    public enum GenderType
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2,
        [Description("DontKnow")]
        DontKnow = 3,
    }
}
