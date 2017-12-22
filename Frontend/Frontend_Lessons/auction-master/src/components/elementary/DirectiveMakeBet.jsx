import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { Button }  from 'antd';

class DirectiveMakeBet extends React.Component {
    static PropTypes = {
        lotId: PropTypes.number
    }
    render() {
        return (
            <Button type="primary" style={{ bottom: '-15px', width: '100%' }}
            >
                <Link to={`/lots/${this.props.lotId}`}>Сделать ставку</Link>
            </Button>
        );
    }
}

export default DirectiveMakeBet;