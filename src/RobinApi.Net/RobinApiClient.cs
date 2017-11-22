using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RobinApi.Net.Exceptions;
using RobinApi.Net.Extensions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;

namespace RobinApi.Net
{ 
    public class RobinApiClient
    {
        private const string ApiBaseUrl = "https://api.robinpowered.com/v1.0/";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Instantiates a new RobinApiClient
        /// </summary>
        /// <param name="apiKey">API key which can be generated on robinpowered.com</param>
        public RobinApiClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Parameter apiKey needs a value");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Access-Token {apiKey}");
        }

        /// <summary>
        /// Get details about your current access token.
        /// </summary>
        /// <returns></returns>
        public async Task<AccessToken> GetAccessTokenDetails()
        {
            var urlBuilder = new StringBuilder("auth");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<AccessToken>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get an organization's details
        /// </summary>
        /// <param name="id">The ID or slug (i.e. username) of the organization</param>
        /// <returns>Returns an Organization resource</returns>
        public async Task<Organization> GetOrganization(string id)
        {
            var urlBuilder = new StringBuilder("organizations/" + id);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Organization>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get all of an organization's locations
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="query">Will filter by a specified location name</param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<Location[]> GetOrganizationLocations(string id, string query = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/locations");
            var parameters = new Dictionary<string, string>
            {
                {"query", query},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()}
            };
            urlBuilder.Append(GetQueryString(parameters));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Location[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Creates a new location for the organization.
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<Location> CreateLocation(string id, Location location)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/locations");
            var content = new StringContent(JsonHelper.Serialize(location), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Location>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get all of users in an organization
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="query">Will filter by a specified user name</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <param name="ids">A list of IDs to retrieve</param>
        /// <returns></returns>
        public async Task<User[]> GetOrganizationUsers(string id, string query = null, int page = 1, int perPage = 10, int[] ids = null)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/users");
            var parameters = new Dictionary<string, string>
            {
                {"query", query},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()}
            };
            if (ids != null)
                parameters.Add("ids", string.Join(",", ids));
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<User[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Invites one or more new users to an organization
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<User[]> AddNewUserToOrganization(string id, User[] users)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/users");
            var content = new StringContent(JsonHelper.Serialize(users), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<User[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get details about an organization's user
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="userId">The ID or slug of the user</param>
        /// <returns></returns>
        public async Task<User> GetOrganizationUser(string id, string userId)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/users/" + userId);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<User>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get details about an organization's amenities
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <returns></returns>
        public async Task<Amenity[]> GetOrganizationAmenities(string id)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/amenities");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Amenity[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Create a new amenity for the organization
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="amenity"></param>
        /// <returns></returns>
        public async Task<Amenity> AddOrganizationAmenity(string id, Amenity amenity)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/amenities");
            var content = new StringContent(JsonHelper.Serialize(amenity), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Amenity>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Returns a list of devices that belong to the Organization resource. This is indicative of hardware that is physically inside the organization, such as a beacon, motion sensor, or projector.
        /// </summary>
        /// <param name="id">The ID or slug of the organization</param>
        /// <param name="manifest">When provided, results will be filtered by the specified device manifest</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <returns></returns>
        public async Task<Device[]> GetOrganizationDevices(int id, string manifest = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("organizations/" + id + "/devices");
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(manifest))
                parameters.Add("manifest", manifest);
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", perPage.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Device[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Gets a User resource.
        /// </summary>
        /// <param name="id">The ID or slug of the user</param>
        /// <returns></returns>
        public async Task<User> GetUser(string id)
        {
            var urlBuilder = new StringBuilder("users/" + id);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<User>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Gets a list of a User's presence history.
        /// </summary>
        /// <param name="id">The ID or slug of the user</param>
        /// <returns></returns>
        public async Task<Presence[]> GetUserPresence(string id)
        {
            var urlBuilder = new StringBuilder("users/" + id + "/presence");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get a user's events
        /// </summary>
        /// <param name="id">The ID or slug of the user</param>
        /// <param name="after">Lower bound for an event's end property</param>
        /// <param name="before">Upper bound for an event's start property</param>
        /// <param name="page">The page to fetch</param>
        /// <param name="perPage">The amount of results to return on a single page.</param>
        /// <returns></returns>
        public async Task<Event[]> GetUserEvents(string id, DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("users/" + id + "/events");
            var parameters = new Dictionary<string, string>();
            if (after.HasValue)
                parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            if (before.HasValue)
                parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", page.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get the current authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetMe()
        {
            var urlBuilder = new StringBuilder("me");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<User>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get the currently authenticated user's presence.
        /// </summary>
        /// <returns></returns>
        public async Task<Presence[]> GetMyPresence()
        {
            var urlBuilder = new StringBuilder("me/presence");

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get a user's events
        /// </summary>
        /// <param name="after">Lower bound for an event's end property</param>
        /// <param name="before">Upper bound for an event's start property</param>
        /// <param name="page">The page to fetch</param>
        /// <param name="perPage">The amount of results to return on a single page.</param>
        /// <returns></returns>
        public async Task<Event[]> GetMyEvents(DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("me/events");
            var parameters = new Dictionary<string, string>();
            if (after.HasValue)
                parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            if (before.HasValue)
                parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", page.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get the currently authenticated user's organizations.
        /// </summary>
        /// <returns></returns>
        public async Task<Organization[]> GetMyOrganizations()
        {
            var urlBuilder = new StringBuilder("me/organizations");

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Organization[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Returns a Location resource, containing information about the location.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <returns>Returns an Location resource</returns>
        public async Task<Location> GetLocation(int id)
        {
            var urlBuilder = new StringBuilder("locations/" + id);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Location>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Updates a Location resource.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task UpdateLocation(Location location)
        {
            var urlBuilder = new StringBuilder("locations/" + location.Id);
            var content = new StringContent(JsonHelper.Serialize(location), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get all of a location's spaces
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="query">Will filter by a specified space name</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <returns></returns>
        public async Task<Space[]> GetLocationSpaces(int id, string query = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/spaces");
            var parameters = new Dictionary<string, string>
            {
                {"query", query},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()}
            };
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Space[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Create a new space in the location.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="space"></param>
        /// <returns></returns>
        public async Task<Space> CreateSpace(int id, Space space)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/spaces");
            var content = new StringContent(JsonHelper.Serialize(space), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Space>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get current presence for a location
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="query">Will filter by a specified space name</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <param name="spaceIds">A list of space IDs to filter by.</param>
        /// <returns></returns>
        public async Task<Presence[]> GetLocationPresence(int id, string query = null, int page = 1, int perPage = 10, int[] spaceIds = null)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/presence");
            var parameters = new Dictionary<string, string>
            {
                {"query", query},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()}
            };
            if (spaceIds != null)
                parameters.Add("spaceIds", string.Join(",", spaceIds));
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Add presence to a space
        /// </summary>
        /// <param name="id">The ID of the location to post presence to.</param>
        /// <param name="presence"></param>
        /// <returns></returns>
        public async Task<Presence> AddLocationPresence(int id, Presence presence)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/presence");
            var content = new StringContent(JsonHelper.Serialize(presence), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Presence>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Expire a user or device's presence in a space
        /// </summary>
        /// <param name="id">The ID of the location to remove presence from.</param>
        /// <param name="presence"></param>
        /// <returns></returns>
        public async Task DeleteLocationPresence(int id, Presence presence)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/presence");
            var content = new StringContent(JsonHelper.Serialize(presence), Encoding.UTF8, "application/json");
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Returns a list of devices that belong to the Location resource. This is indicative of hardware that is physically inside the location, such as a beacon, motion sensor, or projector.
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="manifest">When provided, results will be filtered by the specified device manifest</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <returns></returns>
        public async Task<Device[]> GetLocationDevices(int id, string manifest = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/devices");
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(manifest))
                parameters.Add("manifest", manifest);
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", perPage.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Device[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get a list of a location's events
        /// </summary>
        /// <param name="id">The ID of the location</param>
        /// <param name="after">Lower bound for an event's end property</param>
        /// <param name="before">Upper bound for an event's start property</param>
        /// <param name="page">The page to return</param>
        /// <param name="perPage">The amount of results to return per page</param>
        /// <returns></returns>
        public async Task<Event[]> GetLocationEvents(int id, DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("locations/" + id + "/events");
            var parameters = new Dictionary<string, string>();
            if (after.HasValue)
                parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            if (before.HasValue)
                parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", perPage.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get details about a space
        /// </summary>
        /// <param name="id">The ID of the space to return</param>
        /// <param name="include">Optional submodel includes, such as calendar.</param>
        /// <returns></returns>
        public async Task<Space> GetSpace(int id, string[] include = null)
        {
            var urlBuilder = new StringBuilder("spaces/" + id);
            var parameters = new Dictionary<string, string>();
            if (include != null)
                parameters.Add("include", string.Join(",", include));
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Space>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Update's the properties of an existing Space.
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public async Task UpdateSpace(Space space)
        {
            var urlBuilder = new StringBuilder("spaces/" + space.Id);
            var content = new StringContent(JsonHelper.Serialize(space), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Permanently deletes a Space resource and all of it's related submodels, such as events.
        /// </summary>
        /// <param name="id">The ID of the space to remove</param>
        /// <returns></returns>
        public async Task DeleteSpace(int id)
        {
            var urlBuilder = new StringBuilder("spaces/" + id);
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get a list of a space's events
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="after">Lower bound for an event's end property</param>
        /// <param name="before">Upper bound for an event's start property</param>
        /// <param name="page">The page to return</param>
        /// <param name="perPage">The amount of results to return per page</param>
        /// <returns></returns>
        public async Task<Event[]> GetSpaceEvents(int id, DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/events");
            var parameters = new Dictionary<string, string>();
            if (after.HasValue)
                parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            if (before.HasValue)
                parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", perPage.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Book an event in a space
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task<Event> AddSpaceEvent(int id, Event @event)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/events");
            var content = new StringContent(JsonHelper.Serialize(@event), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Event>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get the amenities for a space.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <returns></returns>
        public async Task<Amenity[]> GetSpaceAmenities(int id)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/amenities");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Amenity[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get the amenities for a space.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="amenityId">The amenity ID to add to the space</param>
        /// <returns></returns>
        public async Task<Amenity> GetSpaceAmenity(int id, int amenityId)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/amenities/" + amenityId);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Amenity>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Adds an amenity to a space.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="amenityId">The amenity ID to add to the space</param>
        /// <returns></returns>
        public async Task<Amenity> AddAmenityToSpace(int id, int amenityId)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/amenities/" + amenityId);
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Amenity>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Removes an amenity from a space.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="amenityId">The amenity ID to remove from the space</param>
        /// <returns></returns>
        public async Task RemoveSpaceAmenity(int id, int amenityId)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/amenities/" + amenityId);
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get any current presence for a space
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="query">Will filter by a specified space name</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <returns></returns>
        public async Task<Presence[]> GetSpacePresence(int id, string query = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/presence");
            var parameters = new Dictionary<string, string>
            {
                {"query", query},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()}
            };
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Add presence to a space
        /// </summary>
        /// <param name="id">The ID of the space to post presence to.</param>
        /// <param name="presence"></param>
        /// <returns></returns>
        public async Task<Presence> AddSpacePresence(int id, Presence presence)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/presence");
            var content = new StringContent(JsonHelper.Serialize(presence), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Presence>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Expire a user or device's presence in a space
        /// </summary>
        /// <param name="id">The ID of the space to remove presence from.</param>
        /// <param name="presence"></param>
        /// <returns></returns>
        public async Task DeleteSpacePresence(int id, Presence presence)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/presence");
            var content = new StringContent(JsonHelper.Serialize(presence), Encoding.UTF8, "application/json");
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Returns a list of devices that belong to the Space resource. This is indicative of hardware that is physically inside the space, such as a beacon, motion sensor, or projector.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="manifest">When provided, results will be filtered by the specified device manifest</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <returns></returns>
        public async Task<Device[]> GetSpaceDevices(int id, string manifest = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/devices");
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(manifest))
                parameters.Add("manifest", manifest);
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", perPage.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Device[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Gets the availability state for the space.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <returns></returns>
        public async Task<SpaceState> GetSpaceState(int id)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/state");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<SpaceState>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Gets the calendar for the space.
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <returns></returns>
        public async Task<Calendar> GetSpaceCalendar(int id)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/calendar");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Meta);
        }

        /// <summary>
        /// Add calendar to a space
        /// </summary>
        /// <param name="id">The ID of the space</param>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public async Task<Calendar> AddSpaceCalendar(int id, Calendar calendar)
        {
            var urlBuilder = new StringBuilder("spaces/" + id + "/calendar");
            var content = new StringContent(JsonHelper.Serialize(calendar), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get an event.
        /// </summary>
        /// <param name="id">The ID of the event</param>
        /// <param name="include">Optional submodel includes, such as confirmation and space.</param>
        /// <returns></returns>
        public async Task<Event> GetEvent(int id, string[] include = null)
        {
            var urlBuilder = new StringBuilder("events/" + id);
            var parameters = new Dictionary<string, string>();
            if (include != null)
                parameters.Add("include", string.Join(",", include));
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Event>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Update's the properties of an existing Event.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task UpdateEvent(Event @event)
        {
            var urlBuilder = new StringBuilder("events/" + @event.Id);
            var content = new StringContent(JsonHelper.Serialize(@event), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Gets the confirmation or checks if a confirmation exists for an event.
        /// </summary>
        /// <param name="id">The ID of the event to get the confirmation for.</param>
        /// <returns></returns>
        public async Task<Confirmation> GetEventConfirmation(int id)
        {
            var urlBuilder = new StringBuilder("events/" + id + "/confirmation");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Confirmation>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Confirms an event.
        /// </summary>
        /// <param name="id">The ID of the event to get the confirmation for.</param>
        /// <param name="confirmation"></param>
        /// <returns></returns>
        public async Task<Confirmation> ConfirmEvent(int id, Confirmation confirmation)
        {
            var urlBuilder = new StringBuilder("events/" + id + "/confirmation");
            var content = new StringContent(JsonHelper.Serialize(confirmation), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Confirmation>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Removes a confirmation for an event.
        /// </summary>
        /// <param name="id">The ID of the event to remove the confirmation for.</param>
        /// <returns></returns>
        public async Task RemoveEventConfirmation(int id)
        {
            var urlBuilder = new StringBuilder("events/" + id + "/confirmation");
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Cancels an event.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns></returns>
        public async Task DeleteEvent(int id)
        {
            var urlBuilder = new StringBuilder("events/" + id);
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Search space availability based on multiple parameters and get back results in a "Best Fit" order
        /// </summary>
        /// <param name="locationIds">One or more location IDs. Will include all spaces in the given locations in the result.</param>
        /// <param name="spaceIds">One or more space IDs. Can be combined with location_ids</param>
        /// <param name="after">Lower bound for free-busy query.</param>
        /// <param name="before">Upper bound for free-busy query.</param>
        /// <param name="duration">The amount of time required between two events for a space to be considered "free".</param>
        /// <param name="types">One or more space types. Unmatched spaces will be filtered out entirely.</param>
        /// <param name="amenityIds">One or more amenity IDs. Unmatched spaces will be filtered out entirely.</param>
        /// <param name="query">Space name filter. Unmatched spaces will be filtered out entirely.</param>
        /// <param name="minCapacity">Filter for min space capacity. Unmatched spaces will be filtered out entirely.</param>
        /// <param name="maxCapacity">Filter for max space capacity. Unmatched spaces will be filtered out entirely.</param>
        /// <param name="page">The page to return</param>
        /// <param name="perPage">The amount of results to return per page</param>
        /// <returns></returns>
        public async Task<FreeBusy[]> GetFreeBusySpaces(int[] locationIds = null, int[] spaceIds = null, DateTime? after = null, DateTime? before = null, int duration = 30, string[] types = null, int[] amenityIds = null, string query = null, int? minCapacity = null, int? maxCapacity = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("free-busy/spaces");
            var parameters = new Dictionary<string, string>();
            if (locationIds != null)
                parameters.Add("location_ids", string.Join(",", locationIds));
            if (spaceIds != null)
                parameters.Add("space_ids", string.Join(",", spaceIds));
            if (after.HasValue)
                parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            if (before.HasValue)
                parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            parameters.Add("duration", duration.ToString());
            if (types != null)
                parameters.Add("types", string.Join(",", types));
            if (amenityIds != null)
                parameters.Add("amenity_ids", string.Join(",", amenityIds));
            parameters.Add("query", query);
            if (minCapacity.HasValue)
                parameters.Add("min_capacity", minCapacity.Value.ToString());
            if (maxCapacity.HasValue)
                parameters.Add("max_capacity", maxCapacity.Value.ToString());
            parameters.Add("page", page.ToString());
            parameters.Add("per_page", perPage.ToString());
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<FreeBusy[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get a list of supported device manifests
        /// </summary>
        /// <param name="page">The page to return</param>
        /// <param name="perPage">The amount of results to return per page</param>
        /// <returns></returns>
        public async Task<DeviceManifest[]> GetDeviceManifests(int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("device-manifests");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<DeviceManifest[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get details about a devicemanifest
        /// </summary>
        /// <param name="id">The ID or slug of the devicemanifest</param>
        /// <returns></returns>
        public async Task<DeviceManifest> GetDeviceManifest(string id)
        {
            var urlBuilder = new StringBuilder("device-manifests/" + id);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<DeviceManifest>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Returns a Device resource, containing information about the device.
        /// </summary>
        /// <param name="id">The ID of the device</param>
        /// <returns>Returns an Device resource</returns>
        public async Task<Device> GetDevice(int id)
        {
            var urlBuilder = new StringBuilder("devices/" + id);
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Device>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Create a new device.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<Device> AddDevice(Device device)
        {
            var urlBuilder = new StringBuilder("me/devices");
            var content = new StringContent(JsonHelper.Serialize(device), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Device>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Add identifier to a device
        /// </summary>
        /// <param name="id">The ID of the device to post identifier to.</param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<Identifier> AddDeviceIdentifier(int id, Identifier identifier)
        {
            var urlBuilder = new StringBuilder("devices/" + id + "/identifiers");
            var content = new StringContent(JsonHelper.Serialize(identifier), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Identifier>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Get all of a device's spaces
        /// </summary>
        /// <param name="id">The ID of the device</param>
        /// <param name="query">Will filter by a specified space name</param>
        /// <param name="page">The page of the result</param>
        /// <param name="perPage">How many results are returned per page</param>
        /// <returns></returns>
        public async Task<Space[]> GetDeviceSpaces(int id, string query = null, int page = 1, int perPage = 10)
        {
            var urlBuilder = new StringBuilder("devices/" + id + "/spaces");
            var parameters = new Dictionary<string, string>
            {
                {"query", query},
                {"page", page.ToString()},
                {"per_page", perPage.ToString()}
            };
            urlBuilder.Append(GetQueryString(parameters));
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper<Space[]>>(jsonResult).Data;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        /// <summary>
        /// Permanently deletes a Device resource and all of it's related submodels.
        /// </summary>
        /// <param name="id">The ID of the device to remove</param>
        /// <returns></returns>
        public async Task DeleteDevice(int id)
        {
            var urlBuilder = new StringBuilder("devices/" + id);
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
        }

        private string GetQueryString(Dictionary<string, string> parameters)
        {
            var queryString = string.Join("&",
                parameters.Select(
                    p =>
                        string.IsNullOrEmpty(p.Value)
                            ? $"{Uri.EscapeDataString(p.Key)}="
                            : $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            return !string.IsNullOrWhiteSpace(queryString) ? "?" + queryString : string.Empty;
        }
    }
}
