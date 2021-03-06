import decodeJwt from 'jwt-decode';
import { fetchUtils } from 'react-admin';

export const httpClient = (url, options = {}) => {
    options.headers = new Headers({ 'Content-Type': 'application/json' });

    const token = localStorage.getItem('token');
    options.user = {
        authenticated: true,
        token: `Bearer ${token}`
    }

    return fetchUtils.fetchJson(url, options);
}

export default {
    login: ({ username, password }) => {
        const request = new Request(apiUrl + '/api/jwt', {
            method: 'POST',
            body: JSON.stringify({ username, password }),
            headers: new Headers({ 'Content-Type': 'application/json' }),
        });
        return fetch(request)
            .then(response => {
                if (response.status < 200 || response.status >= 300) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(({ token }) => {
                const decodedToken = decodeJwt(token);

                localStorage.setItem('token', token);
                localStorage.setItem('username', username);
                localStorage.setItem('permissions', decodedToken[decodedToken.sub]);
            });
    },
    logout: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('permissions');
        return Promise.resolve();
    },
    checkError: error => {
        const { status } = error;
        return (status === 401 || status === 403) ? Promise.reject() : Promise.resolve();
    },
    checkAuth: () => {
        return localStorage.getItem('token') ? Promise.resolve() : Promise.reject();
    },
    getPermissions: () => {
        const role = localStorage.getItem('permissions');
        return role ? Promise.resolve(role) : Promise.resolve();
    }
};