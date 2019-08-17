using Chronicy.Data;
using System;

namespace Chronicy.Web.Converters
{
    public class CustomFieldConverter : IConverter<Data.CustomField, Web.Models.CustomField>
    {
        public bool CanReverseConvert => true;

        public Models.CustomField Convert(Data.CustomField value)
        {
            Models.CustomField field = new Models.CustomField
            {
                ID = value.ID,
                Name = value.Name,
                Type = value.Type.ToString(),
            };

            if (value.Value != null)
            {
                field.Value = value.Value.ToString();
            }

            return field;
        }

        public Data.CustomField ReverseConvert(Models.CustomField value)
        {
            try
            {
                Enum.TryParse(value.Type, out FieldType fieldType);
                Data.CustomField field =  new Data.CustomField
                {
                    ID = value.ID,
                    Name = value.Name,
                    Type = fieldType,
                };

                if (value.Value != null)
                {
                    field.Value = ConvertStringToFieldValue(value.Value, fieldType);
                }

                return field;
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

    public static class CustomFieldConverterExtensions
    {
        public static Models.CustomField ToWebCustomField(this Data.CustomField customField)
        {
            CustomFieldConverter converter = new CustomFieldConverter();
            return converter.Convert(customField);
        }

        public static Data.CustomField ToDataCustomField(this Models.CustomField customField)
        {
            CustomFieldConverter converter = new CustomFieldConverter();
            return converter.ReverseConvert(customField);
        }
    }
}
