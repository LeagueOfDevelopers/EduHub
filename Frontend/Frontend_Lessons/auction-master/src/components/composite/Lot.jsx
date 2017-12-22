import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Tabs } from 'antd';
import LotImage from '../elementary/LotImage';
import LotInfo from '../elementary/LotInfo';
import LotDescription from '../elementary/LotDescription';
import MakeBetInputWithButton from '../elementary/MakeBetInputWithButton';

const TabPane = Tabs.TabPane;

class Lot extends React.Component {
    constructor(props) {
        super(props);
    }
    static PropTypes = {
        lot: PropTypes.object
    }
    static defaultProps = {
        lot: {}
    } 
    render() {
        return (
            <div style={{ backgroundColor: 'rgba(226, 222, 242, 0.2)', borderRadius: 20,
                          padding: 24, margin: '20px', height: '80vh' }}>
                <div style={{ width: '50%', float: 'left', padding: 10 }}>
                    <LotImage />
                </div>
                <div style={{ width: '50%', float: 'right', height: '65vh' }}>
                    <Tabs style={{ color: '#fff' }}>
                        <TabPane tab="Сводка" key="1" style={{ color: '#fff' }}>
                            <LotInfo title={this.props.lot.name} beforeClosing={this.props.lot.expires} betsNumber={this.props.lot.betsCount}/>
                        </TabPane>
                        <TabPane tab="Описание" key="2" style={{ color: '#fff' }}>
                            <LotDescription description={this.props.lot.description}/>
                        </TabPane>
                    </Tabs>
                    
                </div>
                <MakeBetInputWithButton makeBetHandler={this.props.makeBetHandler} lotId={this.props.lot.id} userId={this.props.userId}/>
            </div>
        );
    }
}

export default Lot;