import React, { Component } from 'react';
import { Layout } from 'antd';
import Footer from '../../components/elementary/Footer';
import Lot from '../../components/composite/Lot';
import BackToLotsButton from '../../components/elementary/BackToLotsButton';
import backImg from '../../Pictures/darkPattern.jpg';

import UserProfileButton from '../../components/elementary/UserProfileButton';

import DefineUserModal from '../DefineUserModal/index';

import { fetchData } from './actions'
import { updateLotCurrentPrice } from './actions'
import { connect } from 'react-redux'

const { Header, Content } = Layout;

class LotPage extends React.Component {
    constructor(props) {
        super(props);
    }
    componentDidMount() {
        let id = this.props.match.params.lotId
        this.props.fetchData(id)        
    }
    render() {
        return (
            <Layout style={{ height: '100vh', backgroundImage: `url(${backImg})` }}>
            <Header style={{ background: '#0b0b0c' }}>
                <BackToLotsButton />
                <div style={{ float: 'right', top: '27%', marginRight: '20px' }}>
                    <DefineUserModal />      
                </div>
            </Header>
            <Content style={{ padding: '0 50px' }}>
                <Lot lot={this.props.lot} makeBetHandler={this.props.updateLotCurrentPrice} userId={1}
                    style={{ backgroundColor: '#c7c4d3',
                    borderRadius: 20  }}/>
            </Content>
            <Footer />
          </Layout> 
        );
    }
}

const mapStateToProps = (state) => {
    return {
     lot: state.requiredLot.lot
    }
  }
  
  const mapDispatchToProps = (dispatch) => {
    return {
      fetchData: (id) => {
        dispatch(fetchData(id))
      },
      updateLotCurrentPrice: (lotId, userId, amount) => {
        dispatch(updateLotCurrentPrice(lotId, userId, amount))
      }
    }
  }

export default connect(mapStateToProps, mapDispatchToProps)(LotPage);