using Chronicy.Data;
using System;

namespace Chronicy.Web.Converters
{
    public class CustomFieldConverter : IConverter<Data.CustomField, Web.Models.CustomField>
    {
        public bool CanReverseConvert => true;

        public Models.CustomField Convert(Data.CustomField value)
        {
            return new Models.CustomField
            {
                Name = value.Name,
                Type = value.Type.ToString(),
                Value = value.Value.ToString()
            };
        }

        public Data.CustomField ReverseConvert(Models.CustomField value)
        {
            try
            {
                Enum.TryParse(value.Type, out FieldType fieldType);
                return new Data.CustomField(value.Name, fieldType, ConvertStringToFieldValue(value.Value, fieldType));
            }
            catch (Exception e)
            {
                throw new ConversionException("Could not convert the given argument", e);
            }
        }

        private object ConvertStringToFieldValue(string fieldValueString, FieldType fieldType)
        {
            if (fieldValueString == null)
            {
                throw new ArgumentNullException(nameof(fieldValueString));
            }

            try
            {
                switch (fieldType)
                {
                    case FieldType.Number:
                        return float.Parse(fieldValueString);
                    case FieldType.String:
                        return fieldValueString;
                }
            }
            catch (FormatException e)
            {
                throw new ArgumentException("The value is not convertible to a supported type", nameof(fieldValueString), e);
            }

            throw new ArgumentException("The value is not convertible to a supported type", nameof(fieldValueString));
        }
    }
}
