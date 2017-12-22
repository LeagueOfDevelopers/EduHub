import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { Card, Button }  from 'antd';
import LotImage from '../elementary/LotImage';
import LotSummary from '../elementary/LotSummary';
import DirectiveMakeBet from '../elementary/DirectiveMakeBet';

class Lot extends React.Component {
    constructor(props) {
        super(props);
    }
    static PropTypes = {
        imgSrc: PropTypes.string,
        title: PropTypes.string
    }
    render() {
        return (
            <Card title={this.props.title} bordered={true} style={{ minHeight: '350px', paddingBottom: 10,
                  backgroundColor: '#3c3a44', borderBottomLeftRadius: 10, borderBottomRightRadius: 10, textAlign: 'center' }}>
                <LotImage imgSrc={this.props.imgSrc}/>
                <LotSummary beforeClosing={this.props.beforeClosing} betsNumber={this.props.betsNumber}/>
                <DirectiveMakeBet lotId={this.props.lotId}/>
            </Card>
        );
    }
}

export default Lot;