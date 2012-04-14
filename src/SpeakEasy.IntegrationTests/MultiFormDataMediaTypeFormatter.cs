using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpeakEasy.IntegrationTests
{
    public class MultiFormDataMediaTypeFormatter : FormUrlEncodedMediaTypeFormatter
    {
        public MultiFormDataMediaTypeFormatter()
            : base()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        protected override bool CanReadType(Type type)
        {
            return true;
        }

        protected override bool CanWriteType(Type type)
        {
            return false;
        }

        protected override Task<object> OnReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, FormatterContext formatterContext)
        {
            var contents = formatterContext.Request.Content.ReadAsMultipartAsync().Result;
            return Task.Factory.StartNew<object>(() =>
            {
                return new MultiFormKeyValueModel(contents);
            });
        }

        class MultiFormKeyValueModel : IKeyValueModel
        {
            IEnumerable<HttpContent> _contents;
            public MultiFormKeyValueModel(IEnumerable<HttpContent> contents)
            {
                _contents = contents;
            }


            public IEnumerable<string> Keys
            {
                get
                {
                    return _contents.Cast<string>();
                }
            }

            public bool TryGetValue(string key, out object value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    value = null;
                    return false;
                }

                var thing = _contents.FirstDispositionNameOrDefault(key);

                if (thing == null)
                {
                    value = null;
                    return false;
                }

                value = thing.ReadAsStringAsync().Result;

                return true;
            }
        }
    }
}
