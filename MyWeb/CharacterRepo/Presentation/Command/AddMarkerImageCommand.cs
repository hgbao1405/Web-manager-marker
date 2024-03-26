using CharacterRepo.Data;
using CharacterRepo.Models;
using CharacterRepo.ModelView;
using CharacterRepo.Presentation.Query;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shared.Common;

namespace CharacterRepo.Presentation.Command
{
    public class AddMarkerImageCommand : IRequest<Message>
    {
        public IFormFile file;
        public ImageDTO Image;

        public AddMarkerImageCommand(ImageDTO image, IFormFile file)
        {
            Image = image;
            this.file = file;
        }
    }

    public class AddMarkerImageHandle : IRequestHandler<AddMarkerImageCommand, Message>
    {
        private readonly MyWebContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AddMarkerImageHandle(MyWebContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Message> Handle(AddMarkerImageCommand request, CancellationToken cancellationToken)
        {
            Message mes = await SaveImageAsync(request.file);
            if (mes.Error)
            {
                return mes;
            }

            var I = Image.Convert(request.Image,mes.Title);

            _context.Add(I);
            await _context.SaveChangesAsync(cancellationToken);
            mes.Title = "Tạo ảnh thành công";
            return mes;
        }

        public async Task<Message> SaveImageAsync(IFormFile file)
        {
            Message mess = new Message();
            try
            {
                string webRootPath = _hostEnvironment.WebRootPath;

                // Tạo đường dẫn cho file trong thư mục wwwroot
                if (file == null || file.Length == 0)
                {
                    mess.Error = true;
                    mess.Title = "Đường dẫn không hợp lệ";
                    return mess;
                }

                // Tạo đường dẫn cho file
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(webRootPath, "uploads", fileName);

                // Lưu file vào đường dẫn
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                mess.Title = filePath;
                return mess; // Trả về đường dẫn hoàn chỉnh
            }
            catch (Exception ex)
            {
                mess.Error = true;
                mess.Title = "Lỗi khi lưu hình ảnh: " + ex.Message;
                return mess;
            }
        }
    }
}