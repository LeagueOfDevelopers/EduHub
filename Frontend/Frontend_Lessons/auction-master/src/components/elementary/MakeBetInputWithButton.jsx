import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import { Button, Icon, Tabs, InputNumber } from 'antd';
const TabPane = Tabs.TabPane;

class MakeBetInputWithButton extends React.Component {
    constructor(props) {
        super(props);
    }
    static defaultProps = {
        minBet: 100,
        maxBet: 1000
    }
    static PropTypes = {
        minBet: PropTypes.number,
        maxBet: PropTypes.number
    }
    handleClick = () => {
        console.log(this.props)
        const bet = document.getElementById('betInput').value
        console.log(bet)
        this.props.makeBetHandler(this.props.lotId, this.props.userId, bet)
    }
    render() {
        return(
            <div style={{ height: '13vh', clear: 'both', float: 'right', display: 'flex' }}>
                <InputNumber id='betInput' min={this.props.minBet} max={this.props.maxBet} 
                    defaultValue={this.props.minBet}
                    style={{ minWidth: '130px', minHeight: '7vh', fontSize: '20px', padding: '3%', textAlign: 'justify', left: 20 }}
                />
                <Button type="primary" onClick={this.handleClick}
                    style={{ minHeight: '7vh', fontSize: '20px' }} 
                >
                    Сделать ставку
                </Button>
            </div>            
        );
    }
}

export default MakeBetInputWithButton;