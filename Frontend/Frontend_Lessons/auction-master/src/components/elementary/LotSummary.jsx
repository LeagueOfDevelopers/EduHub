import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { Button }  from 'antd';

class LotSummary extends React.Component {
    static defaultProps = {
        beforeClosing: 0,
        betsNumber: 1
    }
    static PropTypes = {
        beforeClosing: PropTypes.number,
        userId: PropTypes.number
    }
    render() {
        return (
            <div style={{ paddingTop: 10 }}>
                <h2 style={{ color: '#fff' }}>До окончания: {this.props.beforeClosing}</h2>
                <h2 style={{ color: '#fff' }}>Количество ставок: {this.props.betsNumber}</h2>
            </div>
        );
    }
}

export default LotSummary;                
