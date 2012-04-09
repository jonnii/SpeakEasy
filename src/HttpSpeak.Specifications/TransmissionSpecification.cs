using System.Collections.Generic;
using System.Linq;
using HttpSpeak.Serializers;
using Machine.Fakes;
using Machine.Specifications;

namespace HttpSpeak.Specifications
{
    public class TransmissionSpecification
    {
        [Subject(typeof(TransmissionSettings))]
        public class when_getting_supported : with_transmission
        {
            Because of = () =>
                 contentTypes = transmissionSettings.DeserializableMediaTypes;

            It should_have_unique_content_types = () =>
                contentTypes.Count().ShouldEqual(2);

            static IEnumerable<string> contentTypes;
        }

        [Subject(typeof(TransmissionSettings))]
        public class when_finding_deserializer_for_content_type : with_transmission
        {
            Because of = () =>
                deserializer = transmissionSettings.FindSerializer("application/json");

            It should_find_deserializer = () =>
                deserializer.ShouldBeTheSameAs(firstDeserializer);

            static ISerializer deserializer;
        }

        [Subject(typeof(TransmissionSettings))]
        public class when_finding_deserializer_for_content_type_that_isnt_registered : with_transmission
        {
            Because of = () =>
                deserializer = transmissionSettings.FindSerializer("application/fribble");

            It should_return_null_deserializer = () =>
                deserializer.ShouldBeOfType<NullSerializer>();

            static ISerializer deserializer;
        }

        public class with_transmission : WithFakes
        {
            Establish context = () =>
            {
                firstDeserializer = An<ISerializer>();
                firstDeserializer.WhenToldTo(r => r.SupportedMediaTypes).Return(new[] { "application/json" });

                secondDeserializer = An<ISerializer>();
                secondDeserializer.WhenToldTo(r => r.SupportedMediaTypes).Return(new[] { "text/xml", "application/json" });

                transmissionSettings = new TransmissionSettings(new[] { firstDeserializer, secondDeserializer });
            };

            protected static TransmissionSettings transmissionSettings;

            protected static ISerializer firstDeserializer;

            protected static ISerializer secondDeserializer;
        }
    }
}
