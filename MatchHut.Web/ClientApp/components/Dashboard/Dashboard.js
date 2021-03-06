import React, { Component } from 'react';
import Welcome from './Welcome';

const styles = {
    welcome: { marginBottom: '2em' },
    flex: { display: 'flex' },
    flexColumn: { display: 'flex', flexDirection: 'column' },
    leftCol: { flex: 1, marginRight: '1em' },
    rightCol: { flex: 1, marginLeft: '1em' },
    singleCol: { marginTop: '2em', marginBottom: '2em' },
};

class Dashboard extends Component {
    render() {
        return (
            <div style={styles.flexColumn}>
                <div style={styles.singleCol}>
                    <Welcome />
                </div>
            </div>
        );
    }
}

export default Dashboard;