using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.DtoModels
{
    public class PageDto<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }

        public PageDto() { }
        public PageDto(List<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }

    }

}