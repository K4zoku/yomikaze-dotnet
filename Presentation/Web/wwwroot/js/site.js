document.addEventListener('alpine:init', () => {
    Alpine.store('auth', {
        token: localStorage.getItem('token'),
        http: null,

        init() {
            this.http = axios.create({
                baseURL: '/',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'Authorization': this.token ? `Bearer ${this.token}` : undefined
                }
            });
        },

        get authenticated() {
            return !!this.token;
        },

        async getProfile() {
            if (!this.authenticated) return null;
            let p = sessionStorage.getItem('profile')
            if (p) {
                this._profile = JSON.parse(p);
                console.log("Profile from session storage");
                return this._profile;
            }
            console.log("Session storage not storing profile data, fetching profile from server...");
            let data = await this.http.get('/API/Authenticate/Info')
                .then((response) => {
                    let responseObject = response.data;
                    return responseObject.data || {profile: null};
                }).catch((error) => {
                    console.log("Error fetching profile");
                    console.error(error);
                    return null;
                });
            let profile = data.profile;
            if (!profile) {
                console.log("No profile found in response data", data);
                return null;
            }
            if (!profile.avatar) {
                console.log("No avatar found in profile, generating gravatar...");
                let emailHash = await sha256(profile.email);
                profile.avatar = `https://gravatar.com/avatar/${emailHash}?d=mp&f=y`;
            }
            this.profile = profile;
            return profile;
        },

        _profile: null,

        get profile() {
            return this._profile;
        },

        set profile(value) {
            this._profile = value;
            sessionStorage.setItem('profile', JSON.stringify(value));
        },

        login(token) {
            this.token = token
            localStorage.setItem('token', token);
            this.http.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        },

        logout() {
            this.token = null
            localStorage.removeItem('token');
            sessionStorage.removeItem('profile');
            this.http.defaults.headers.common['Authorization'] = undefined;
            window.location.reload();
        },
    });

    Alpine.data('theme', () => ({
        init() {
            this.$watch('themeId', (value) => localStorage.setItem('theme', value))
        },
        themeId: parseInt(localStorage.getItem('theme')) || 0,
        themes: [
            {
                theme: 'ayu-light',
                name: 'Ayu Light',
                dark: false,
                icon: 'sun',
            },
            {
                theme: 'ayu-mirage',
                name: 'Ayu Mirage',
                dark: true,
                icon: 'sunset',
            },
            {
                theme: 'ayu-dark',
                name: 'Ayu Dark',
                dark: true,
                icon: 'moon',
            },
            {
                theme: 'catpuccin-latte',
                name: 'Catpuccin Latte',
                dark: false,
                icon: 'sunrise',
            },
            {
                theme: 'catpuccin-frappe',
                name: 'Catpuccin Frappe',
                dark: true,
                icon: 'droplet',
            },
            {
                theme: 'catpuccin-macchiato',
                name: 'Catpuccin Macchiato',
                dark: false,
                icon: 'filter',
            },
            {
                theme: 'catpuccin-mocha',
                name: 'Catpuccin Mocha',
                dark: false,
                icon: 'star',
            }
        ],
        nextTheme() {
            this.themeId = (this.themeId + 1) % this.themes.length;
        },
        get currentTheme() {
            return this.themes[this.themeId]
        },
        get theme() {
            return this.currentTheme.theme
        },
        get isDark() {
            return this.currentTheme.dark
        },
        get themeIcon() {
            return this.currentTheme.icon
        }
    }));
});

document.addEventListener('alpine:initialized', () => {
    console.log("Alpine initialized");
})

window.sha256 = async (input) => {
    const textAsBuffer = new TextEncoder().encode(input);
    const hashBuffer = await window.crypto.subtle.digest("SHA-256", textAsBuffer);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    return hashArray
        .map((item) => item.toString(16).padStart(2, "0"))
        .join("");
};

window.getFirstError = (data, errorField) => [].concat((data?.errors ?? {})[errorField] || []).at(0) || '';

URLSearchParams.prototype.addODataPagination = function(current, size) {
    this.set('$top', size.toString());
    this.set('$skip', ((current - 1) * size).toString());
}

window.camelize = obj => _.transform(obj, (acc, value, key, target) => {
    const camelKey = _.isArray(target) ? key : _.camelCase(key);
    acc[camelKey] = _.isObject(value) ? camelize(value) : value;
});

window.API_BASE_URL = window.location.origin + '/API';
window.ODATA_BASE_URL = API_BASE_URL + '/OData';