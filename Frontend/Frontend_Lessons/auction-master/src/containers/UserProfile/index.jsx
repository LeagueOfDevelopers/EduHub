import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Layout, Tabs } from 'antd';
import backImg from '../../Pictures/darkPattern.jpg';
import Footer from '../../components/elementary/Footer';
import MadeBetsTable from '../../components/elementary/MadeBetsTable';
import BackToLotsButton from '../../components/elementary/BackToLotsButton';
import ExitButton from '../../components/elementary/ExitButton';
import Profile from '../../components/composite/Profile';
import { fetchData } from './actions';
import { connect } from 'react-redux';

const { Header, Content } = Layout;
const TabPane = Tabs.TabPane;

class UserProfile extends React.Component {
    constructor(props) {
        super(props);
    }
    componentDidMount() {
        let id = this.props.match.params.userId
        this.props.fetchData(id)
    }

    componentDidUpdate(prevProps, prevState) {
        console.log(this.props.user)
    }

    render() {
        return (
            <Layout style={{ height: '100vh', backgroundImage: `url(${backImg})` }}>
                <Header style={{ background: '#0b0b0c' }}>
                    <BackToLotsButton />
                    <ExitButton />
                </Header>
                <Content style={{ margin: '24px 70px', padding: "0 50px", minHeight: 280,
                                  backgroundColor: 'rgba(226, 222, 242, 0.2)', borderRadius: 20 }}>
                    <Tabs style={{ color: '#fff', padding: 20 }}>
                        <TabPane tab="Профиль" key="1">
                            <Profile /*login={this.props.user.name} balance={this.props.user.account}*/ user={this.props.user}/>
                        </TabPane>
                        <TabPane tab="Лоты под ставкой" key="2">
                            <MadeBetsTable />
                        </TabPane>
                        <TabPane tab="Мои лоты" key="3">
                            
                        </TabPane>
                    </Tabs>
                </Content>
                <Footer />
            </Layout> 
        );
    }
}

const mapStateToProps = (state) => {
    return {
      user: state.currentUser.user
    }
}
  
  const mapDispatchToProps = (dispatch) => {
    return {
      fetchData: (userId) => {
        dispatch(fetchData(userId))
      }
    }
}
  
export default connect(mapStateToProps, mapDispatchToProps)(UserProfile);