import React, { Component } from 'react';
import { connect } from 'react-redux';
import 'antd/dist/antd.css';
import { Layout, Menu, Button, BackTop } from 'antd';
import { Link } from 'react-router-dom';
import backImg from '../../Pictures/darkPattern.jpg';

import LotsGrid from '../../components/composite/LotsGrid'
import SearchBar from '../../components/elementary/SearchBar';
import Footer from '../../components/elementary/Footer';

import UserProfileButton from '../../components/elementary/UserProfileButton';

import LotsPagination from '../../components/elementary/LotsPagination';
//import DefineUserModal from '../DefineUserModal/index';
import { fetchData } from './actions';

const { Header, Content, Sider } = Layout;

class LotsListPage extends Component {
  state = {
    collapsed: false,
  };
  componentDidMount() {
    this.props.fetchData()
  }
  onCollapse = (collapsed) => {
    console.log(collapsed);
    this.setState({ collapsed });
  }
    render() {
      return(
        <Layout style={{ minHeight: '100vh' }}>
        <Layout style={{ position: 'relative', backgroundImage: `url(${backImg})`}}>
          <Header style={{ background: '#0b0b0c', padding: 0 }}>
            <SearchBar />
            <div style={{ float: 'right', top: '27%', marginRight: '20px' }}>
            <UserProfileButton />      
            </div>
          </Header>
          <Content style={{ margin: '24px 16px', padding: 24, 
                            minHeight: 280, backgroundColor: 'rgba(226, 222, 242, 0.2)',
                            borderRadius: 20  }}>
            <LotsGrid lots={this.props.lots} currentUserId={1}/>
            <BackTop style={{ right: '20px' }}/>
          </Content>
          <Footer />
        </Layout>
      </Layout>
    );
  }
}


const mapStateToProps = (state) => {
  return {
    lots: state.lotsList.lots,
    currentUserId: state.currentUser.user.id
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    fetchData: () => {
      dispatch(fetchData())
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(LotsListPage);