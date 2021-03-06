import React from 'react';
import { Route } from 'react-router';
import Configuration from './components/Configuration';
import Company from './components/Company';
import ForgotPassword from './ForgotPassword';

export default [
    <Route exact path="/configurations" component={Configuration} />,
    <Route exact path="/company" component={Company} />,
    <Route exact path="/forgotpassword" component={ForgotPassword} noLayout />
];