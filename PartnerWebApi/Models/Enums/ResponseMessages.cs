namespace PartnerWebApi.Models.Enums
{
    public class ResponseMessages
    {
        public static string TranslateMessage(Messages msg)
        {
            switch (msg)
            {
                case Messages.InvalidIdNumber:
                    return "מספר ת.ז. אינו תקין";
                case Messages.CustomerDatailsUnavailable:
                    return "לא ניתן לקבל את פרטי הלקוח";
                case Messages.CustomerNotExist:
                    return "לקוח לא קיים במערכת";
                case Messages.DataError:
                    return "שגיאת נתונים";
                case Messages.CustomerUpdatedFailed:
                    return "עדכון לקוח נכשל";
                case Messages.UpdateAddressSuccess:
                    return "כתובת עודכנה בהצלחה!";
                default:
                    return "";
            }
        }
        public enum Messages
        {
            InvalidIdNumber,
            CustomerDatailsUnavailable,
            CustomerNotExist,
            DataError,
            CustomerUpdatedFailed,
            UpdateAddressSuccess,
            Other
        }
    }
}