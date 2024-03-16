document.addEventListener('alpine:init', () => {
    console.log("Alpine init")
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
            console.log("Auth init")
        },
        
        get authenticated() {
            return !!this.token;
        },
        
        getProfile() {
            return this.authenticated ? this.http.get('/API/Authenticate/Info') : Promise.reject('Not authenticated');
        },

        login(token) {
            this.token = token
            localStorage.setItem('token', token);
            this.http.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        },

        logout() {
            this.token = null
            localStorage.removeItem('token');
            this.http.defaults.headers.common['Authorization'] = undefined;
            window.location.reload();
        },
    });
    
    Alpine.data('theme', () => ({
        init() {
            this.$watch('id', (value) => localStorage.setItem('theme', value))
        },
        id: parseInt(localStorage.getItem('theme')) || 0,
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
            this.id = (this.id + 1) % this.themes.length;
        },
        get currentTheme() {
            return this.themes[this.id]
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
    console.log("Alpine initialized")
})

