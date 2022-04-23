using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlaceAgregator.Entities
{
    public class Photo : Entity
    {
        public byte[] Bytes { get; private set; }
        public string Description { get; private set; }
        public string FileExtension { get; private set; }

        public IEnumerable<PlacePhoto> PlacePhotos { get; private set; }

        private Photo() { }
        public Photo GetPhotoFromFormFile(FormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                throw new ArgumentNullException(nameof(formFile));
            }
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                Photo photo = new Photo(
                    bytes: memoryStream.ToArray(),
                    desciprion: formFile.FileName,
                    fileExtension: Path.GetExtension(formFile.FileName)
                    );
                return photo;
            }
        }
        public Photo(byte[] bytes, string desciprion, string fileExtension)
        {
            Bytes = bytes;
            Description = desciprion;
            FileExtension = fileExtension;
        }
    }
}
