using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using SpeakEasy.Serializers;

namespace SpeakEasy.Specifications
{
    [Subject(typeof(TransmissionSettings))]
    class TransmissionSpecs : WithFakes
    {
        protected static TransmissionSettings transmissionSettings;

        protected static ISerializer firstDeserializer;

        protected static ISerializer secondDeserializer;

        Establish context = () =>
        {
            firstDeserializer = An<ISerializer>();
            firstDeserializer.WhenToldTo(r => r.SupportedMediaTypes).Return(new[] { "application/json" });

            secondDeserializer = An<ISerializer>();
            secondDeserializer.WhenToldTo(r => r.SupportedMediaTypes).Return(new[] { "text/xml", "application/json" });

            transmissionSettings = new TransmissionSettings(new[] { firstDeserializer, secondDeserializer });
        };

        class when_getting_supported
        {
            Because of = () =>
                 contentTypes = transmissionSettings.DeserializableMediaTypes;

            It should_have_unique_content_types = () =>
                contentTypes.Count().ShouldEqual(2);

            static IEnumerable<string> contentTypes;
        }

        class when_finding_deserializer_for_content_type
        {
            static ISerializer deserializer;

            Because of = () =>
                deserializer = transmissionSettings.FindSerializer("application/json");

            It should_find_deserializer = () =>
                deserializer.ShouldBeTheSameAs(firstDeserializer);
        }

        class when_finding_deserializer_for_content_type_that_isnt_registered
        {
            static ISerializer deserializer;

            Because of = () =>
                deserializer = transmissionSettings.FindSerializer("application/fribble");

            It should_return_null_deserializer = () =>
                deserializer.ShouldBeOfExactType<NullSerializer>();
        }
    }
}
