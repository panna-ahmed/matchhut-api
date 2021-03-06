import React, { useState } from 'react';
import { connect } from 'react-redux';
import compose from 'recompose/compose';
import SubMenu from './SubMenu';

import { withRouter } from 'react-router-dom';
import {
    translate,
    DashboardMenuItem,
    MenuItemLink,
    usePermissions 
} from 'react-admin';

import LabelIcon from '@material-ui/icons/LibraryBooks';
import { CompanyIcon } from './components/Company';
import { ConfigurationIcon } from './components/Configuration';
import { RoleIcon } from './components/Role';
import { UserIcon } from './components/User';
import { RoleClaimIcon } from './components/RoleClaim';
import { UserRoleIcon } from './components/UserRole';

import MenuItem from '@material-ui/core/MenuItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import Typography from '@material-ui/core/Typography';

import green from '@material-ui/core/colors/green';
import { withStyles } from '@material-ui/core/styles';

const styles = {
    icon: {
        color: green[900],
        minWidth: 40
    },
};

export const GreenListItemIcon = withStyles(styles)(({ classes, ...props }) => (
    <ListItemIcon className={classes.icon} {...props} />
));

const Menu = ({ onMenuClick, logout, open, translate }) => {

    const [menuUsers, setMenuUsers] = useState(false);
    const [menuSettings, setMenuSettings] = useState(false);
    
    const { permissions } = usePermissions();

    return (
        <div>
            {' '}
            {permissions?.includes('company') &&
                <MenuItemLink
                    to={`/company`}
                    primaryText={translate(`resources.company.name`, {
                        smart_count: 2,
                    })}
                    leftIcon={<CompanyIcon />}
                    onClick={onMenuClick}
                />
            }
            <DashboardMenuItem onClick={onMenuClick} sidebarIsOpen={open} />
            
            
            {permissions?.includes('user') &&
                <SubMenu
                    handleToggle={() => setMenuUsers(!menuUsers)}
                    isOpen={menuUsers}
                    sidebarIsOpen={open}
                    name={translate(`resources.users.name`, {
                        smart_count: 2,
                    })}
                    icon={<LabelIcon />}
                >
                    <MenuItemLink
                        to={`/users`}
                        primaryText={translate(`resources.users.name`, {
                            smart_count: 2,
                        })}
                        leftIcon={<UserIcon />}
                        onClick={onMenuClick}
                    />
                    <MenuItemLink
                        to={`/roles`}
                        primaryText={translate(`resources.roles.name`, {
                            smart_count: 2,
                        })}
                        leftIcon={<RoleIcon />}
                        onClick={onMenuClick}
                    />
                    <MenuItemLink
                        to={`/userroles`}
                        primaryText={translate(`resources.userroles.name`, {
                            smart_count: 2,
                        })}
                        leftIcon={<UserRoleIcon />}
                        onClick={onMenuClick}
                    />
                    <MenuItemLink
                        to={`/roleclaims`}
                        primaryText={translate(`resources.roleclaims.name`, {
                            smart_count: 2,
                        })}
                        leftIcon={<RoleClaimIcon />}
                        onClick={onMenuClick}
                    />
                </SubMenu>
            }

            {permissions?.includes('config') &&
                <SubMenu
                    handleToggle={() => setMenuSettings(!menuSettings)}
                    isOpen={menuSettings}
                    sidebarIsOpen={open}
                    name={translate(`resources.settings.name`, {
                        smart_count: 2,
                    })}
                    icon={<LabelIcon />}
                >
                    <MenuItemLink
                        to={`/configurations`}
                        primaryText={translate(`resources.configurations.name`, {
                            smart_count: 2,
                        })}
                        leftIcon={<ConfigurationIcon />}
                        onClick={onMenuClick}
                    />
                </SubMenu>
            }

            
        </div>
    );
}

const mapStateToProps = state => ({
    open: state.admin.ui.sidebarOpen,
    theme: state.theme,
});

const enhance = compose(
    withRouter,
    connect(
        mapStateToProps,
        {}
    ),
    translate
);

export default enhance(Menu);