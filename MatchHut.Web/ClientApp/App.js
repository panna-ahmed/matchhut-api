import React from 'react';
import { Admin, Resource } from 'react-admin';
import simpleRestProvider from 'ra-data-simple-rest';

import Menu from './Menu';
import authProvider, { httpClient } from './custom/authProvider';

import { RoleList, RoleEdit, RoleCreate } from './components/Role';
import { UserList, UserEdit, UserCreate } from './components/User';
import { UserRoleList, UserRoleEdit, UserRoleCreate } from './components/UserRole';
import { RoleClaimList, RoleClaimEdit, RoleClaimCreate } from './components/RoleClaim';

import { Dashboard } from './components/Dashboard/';

import polyglotI18nProvider from 'ra-i18n-polyglot';

import englishMessages from './i18n/en';
import customRoutes from './routes';
import Login from './Login';

import axios from 'axios';

const messages = {
    fr: () => import('./i18n/fr.js').then(messages => messages.default),
};

const i18nProvider = polyglotI18nProvider(locale => {
    if (locale === 'fr') {
        return messages[locale]();
    }

    // Always fallback on english
    return englishMessages;
}, 'en');

axios.interceptors.request.use(function (config) {
    const token = localStorage.getItem('token');
    config.headers.authorization = `Bearer ${token}`;

    return config;
});

const dataProvider = simpleRestProvider(apiUrl + '/api', httpClient);

const App = () => (
    <Admin title="MatchHut 1.0.0"
        loginPage={Login}
        dataProvider={dataProvider}
        menu={Menu}
        customRoutes={customRoutes}
        authProvider={authProvider}
        i18nProvider={i18nProvider}
        dashboard={Dashboard}>
        {permissions => [
            permissions?.includes('user') ?
                [
                    <Resource name="roles" list={RoleList} edit={RoleEdit} create={RoleCreate} options={{ label: 'Role' }} />,
                    <Resource name="users" list={UserList} edit={UserEdit} create={UserCreate} options={{ label: 'User' }} />,
                    <Resource name="userroles" list={UserRoleList} edit={UserRoleEdit} create={UserRoleCreate} options={{ label: 'User Role' }} />,
                    <Resource name="roleclaims" list={RoleClaimList} edit={RoleClaimEdit} create={RoleClaimCreate} options={{ label: 'Role Claim' }} />
                ] : null,
            <Resource name="claims" />
        ]}
    </Admin>
);

export default App;