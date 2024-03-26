using Shared.Common;

namespace Shared.Archetype
{
    public class archetype
    {
        public archetype()
        {
            CreatedTime = DateTime.Now;
            IsDeleted = false;
        }

        public int id { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }

        public Message Delete(string NameObject)
        {
            var mes = new Message();

            if (IsDeleted == true)
            {
                mes.Error = true;
                mes.Title = NameObject + " này đã xóa";
                return mes;
            }

            IsDeleted = true;
            DeletedTime = DateTime.Now;

            mes.Title = "Xóa thành công";

            return mes;
        }
        public static Message CheckAndDelete(archetype character, string NameObject)
        {
            if (character == null)
            {
                var mes = new Message
                {
                    Error = true,
                    Title = "Không tìm thấy " + NameObject
                };
                return mes;
            }

            return character.Delete(NameObject);
        }
    }
}