using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Resticle.Deserializers;

namespace Resticle.Specifications
{
    public class TransmissionSpecification
    {
        [Subject(typeof(Transmission))]
        public class when_getting_supported : with_transmission
        {
            Because of = () =>
                 contentTypes = transmission.DeserializableMediaTypes;

            It should_have_unique_content_types = () =>
                contentTypes.Count().ShouldEqual(2);

            static IEnumerable<string> contentTypes;
        }

        [Subject(typeof(Transmission))]
        public class when_finding_deserializer_for_content_type : with_transmission
        {
            Because of = () =>
                deserializer = transmission.FindDeserializer("application/json");

            It should_find_deserializer = () =>
                deserializer.ShouldBeTheSameAs(firstDeserializer);

            static IDeserializer deserializer;
        }

        [Subject(typeof(Transmission))]
        public class when_finding_deserializer_for_content_type_that_isnt_registered : with_transmission
        {
            Because of = () =>
                deserializer = transmission.FindDeserializer("application/fribble");

            It should_return_null_deserializer = () =>
                deserializer.ShouldBeOfType<NullDeserializer>();

            static IDeserializer deserializer;
        }

        public class with_transmission : WithFakes
        {
            Establish context = () =>
            {
                serializer = An<ISerializer>();
                firstDeserializer = An<IDeserializer>();
                firstDeserializer.WhenToldTo(r => r.SupportedMediaTypes).Return(new[] { "application/json" });

                secondDeserializer = An<IDeserializer>();
                secondDeserializer.WhenToldTo(r => r.SupportedMediaTypes).Return(new[] { "text/xml", "application/json" });

                transmission = new Transmission(serializer, new[] { firstDeserializer, secondDeserializer });
            };

            protected static Transmission transmission;

            protected static ISerializer serializer;

            protected static IDeserializer firstDeserializer;

            protected static IDeserializer secondDeserializer;
        }
    }
}
