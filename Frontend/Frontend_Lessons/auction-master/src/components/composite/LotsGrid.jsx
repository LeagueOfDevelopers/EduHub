import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Row, Col } from 'antd';
import LotCard from './LotCard';

class LotsGrid extends React.Component {
    constructor(props) {
        super(props);
    }
    static PropTypes = {
        lots: PropTypes.array,
        currentUserId: PropTypes.number
    }
    static defaultProps = {
        lots: [],
        currentUserId: 1
    } 
      render() 
      {
        return (
             this.props.lots.map((lot) => {
                 if (lot.autorId !== this.props.currentUserId) {
                     return(
                        <Row style={{ marginBottom: '15px', display: 'flex' }}>
                        <Col span={12} offset={6}>
                            <LotCard title={lot.name} 
                                     betsNumber={lot.betsCount} 
                                     beforeClosing={lot.expires} 
                                     betsNumber={lot.betsCount} 
                                     lotId={lot.id}
                                />
                        </Col>
                    </Row>
                )
            }
        }
    ));
}
}



export default LotsGrid;             

/*
            <Row gutter={16} style={{ marginBottom: '15px', display: 'flex' }}>
            <Col span={8}>
                <LotCard title={this.props.title} betsNumber={this.props.betsNumber}/>
            </Col>
            <Col span={8}>
                <LotCard title={this.props.title} betsNumber={this.props.betsNumber}/>
            </Col>
            <Col span={8}>
                <LotCard title={this.props.title} betsNumber={this.props.betsNumber}/>
            </Col>
          </Row>
          */
        