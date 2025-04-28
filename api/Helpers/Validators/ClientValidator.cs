// Ignore Spelling: Validators

using api.DTOs;

namespace api.Helpers.Validators
{
    public static class ClientValidator
    {
        public static  bool IsEnterAllInput<T>(T inputDto) where T : IClientDTO
        {
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(inputDto);

                if ( string.IsNullOrWhiteSpace((string?)value) || string.IsNullOrEmpty((string?)value))
                   return false;
            }

            return true;
        }

    }
}
