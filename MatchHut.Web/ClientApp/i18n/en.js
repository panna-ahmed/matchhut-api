import englishMessages from 'ra-language-english';

export default {
    ...englishMessages,
    
    ['Not Found']: 'Not found',
    ['Configuration updated']: 'Configuration updated',
    ['Unauthorized']: 'Unauthorized',
    errors: {
    },
    resources: { 
        company: {
            name: 'Company'
        },
        settings: {
            name: 'Settings'
        },
        configurations: {
            name: 'Configurations'
        },
        roles: {
            name: 'Roles',
            error: 'Getting roles failed.'
        },
        users: {
            name: 'Users',
            unique: 'Username already exists.'
        },
        userroles: {
            name: 'User Roles'
        },
        roleclaims: {
            name: 'Role Claims',
            error: 'Assigning claims to the role failed.',
            success: 'Assignment finished successfully.',
            unique: 'This role claim already exists.'
        },
    }
};