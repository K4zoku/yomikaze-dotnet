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
        
        getProfile() {
            if (!this.authenticated) return null;
            let p = localStorage.getItem('profile');
            if (p) {
                console.log("Profile from local storage");
                return JSON.parse(p);
            }
            console.log("Local storage empty, fetching profile from server...");
            return this.http.get('/API/Authenticate/Info')
                .then(response => {
                    let responseObject = response.data;
                    let data = responseObject.data;
                    if (!data.profile) {
                        console.log("No profile found in response data", response);
                        return null;
                    }
                    localStorage.setItem('profile', JSON.stringify(data.profile));
                    return data.profile;
                }).catch((error) => {
                    console.log("Error fetching profile");
                    console.error(error);
                    return null;
                });   
        },

        login(token) {
            this.token = token
            localStorage.setItem('token', token);
            this.http.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        },

        logout() {
            this.token = null
            localStorage.removeItem('token');
            localStorage.removeItem('profile');
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
    Alpine.store('auth').getProfile();
})

