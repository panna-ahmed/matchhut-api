import React from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import { Title } from 'react-admin';

export default () => (
    <Card>
        <Title title="MatchHut" />
        <CardContent>
            <h3>{companyName}</h3>
            {companyAddress && <div>{companyAddress}</div>}
            {companyAddress2 && <div>{companyAddress2}</div>}
            {companyPhone && <div>{companyPhone}</div>}
            {companyEmail && <div>{companyEmail}</div>}
            {companyWebsite && <div>{companyWebsite}</div>}
        </CardContent>

        <CardContent>
            <div style={{
                fontSize: 10
            }}>Developed by:</div>
            
            <div style={{
                fontWeight: 'bold'
            }}>Ignytek</div>
            <div>Dhaka, Bangladesh</div>
        </CardContent>
    </Card>
);