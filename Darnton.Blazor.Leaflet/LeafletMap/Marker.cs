using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Darnton.Blazor.Leaflet.LeafletMap
{
    /// <summary> A clickable, draggable icon that can be added to a <see cref="Map"/><see href="https://leafletjs.com/reference-1.7.1.html#marker"/> </summary>
    public class Marker : InteractiveLayer
    {
        /// <summary> Constructs a marker </summary>
        /// <param name="latlng"> The initial position of the marker. </param>
        /// <param name="options"> The <see cref="MarkerOptions"/> used to create the marker. </param>
        public Marker(LatLng latlng, MarkerOptions options)
        {
            LatLng = latlng;
            Options = options;
        }

        /// <summary> The initial position of the marker. </summary>
        [JsonIgnore] public LatLng LatLng { get; private set; }

        /// <summary> The <see cref="MarkerOptions"/> used to create the marker. </summary>
        [JsonIgnore] public MarkerOptions Options { get; }

        public async ValueTask SetLatLng(LatLng latLng)
        {
            LatLng = latLng;
            await JSObjectReference.InvokeAsync<IJSObjectReference>("setLatLng", latLng);
        }

        /// <inheritdoc/>
        protected override async Task<IJSObjectReference> CreateJsObjectRef()
        {
            return await JSBinder.JSRuntime.InvokeAsync<IJSObjectReference>("L.marker", LatLng, Options);
        }
    }
}