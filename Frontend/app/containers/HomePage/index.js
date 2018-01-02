/*
 * HomePage
 *
 * This is the first thing users see of our App, at the '/' route
 *
 * NOTE: while this component should technically be a stateless functional
 * component (SFC), hot reloading does not currently support SFCs. If hot
 * reloading is not a necessity for you then you can refactor it and remove
 * the linting exception.
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import { makeSelectHomePage,
         makeSelectAssembledGroups,
         makeSelectUnassembledGroups
} from "./selectors";
import reducer from './reducer';
import saga from './saga';
import {Card, Col, Row, Button, message} from 'antd';
// import styled from 'styled-components';

import Header from 'components/Header';
import UnassembledGroupCard from 'components/UnassembledGroupCard';
import AssembledGroupCard from 'components/AssembledGroupCard';
import SigningInForm from 'components/SigningInForm';
import {Link} from "react-router-dom";
import { getAssembledGroups, getUnassembledGroups } from "./actions";

export class HomePage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.makeTeacher = this.makeTeacher.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleOk = this.handleOk.bind(this);

    this.state = {
      signInVisible: false
    }
  }

  componentDidMount() {
    this.props.getUnassembledGroups();
    this.props.getAssembledProps();
  }

  makeTeacher() {
    if(this.props.token)
      message.success('Теперь вы можете преподавать!');
    else
      this.setState({signInVisible: true})
  }

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  handleOk = () => {
    message.error('Не удалось войти!')
  };

  unassembledGroups = (
    <div className='cards-holder'>
      {this.props.unassembledGroups.map((item) =>
        <Link to='/group'>
          <UnassembledGroupCard {...item}/>
        </Link>
      )}
    </div>
  );

  assembledGroups = (
    <div className='cards-holder'>
      {this.props.assembledGroups.map((item) =>
        <Link to='/group'>
          <AssembledGroupCard {...item}/>
        </Link>
      )}
    </div>
  )

  render() {
    return (
      <div>
        <header>
          <Header token={this.props.token}/>
        </header>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Незаполненные группы'
            bordered={false}
            className='unassembled-groups-list font-size-20'
            extra={<Link to='#'>Показать больше</Link>}
          >
            {this.props.unassembledGroups.length > 0 ?
              this.unassembledGroups
              : (<div>Здесь пока ничего нет.</div>)
            }
            <Row type='flex' justify='end' align='middle' style={{marginTop: 30}}>
              <Col style={{fontSize: 18, marginRight: '2%'}}>Не нашли то, что искали?</Col>
              <Link to='/create_group'><Button type="primary" htmlType="submit">Создать группу</Button></Link>
            </Row>
          </Card>
        </Col>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Заполненные группы'
            bordered={false}
            className='assembled-groups-list font-size-20'
            extra={<Link to='#'>Показать больше</Link>}
          >
            {this.props.unassembledGroups.length > 0 ?
              this.assembledGroups
              : (<div>Здесь пока ничего нет.</div>)
            }
            <Row type='flex' justify='end' align='middle' style={{marginTop: 30}}>
              <Col style={{fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
              <Button type="primary" htmlType="submit" onClick={this.makeTeacher}>Стать преподавателем</Button>
              <SigningInForm visible={this.state.signInVisible} handleOk={this.handleOk} handleCancel={this.handleCancel}/>
            </Row>
          </Card>
        </Col>
      </div>
    );
  }
}

HomePage.propTypes = {
  dispatch: PropTypes.func.isRequired,
  makeTeacher: PropTypes.func,
  unassembledGroups: PropTypes.array,
  assembledGroups: PropTypes.array
};

HomePage.defaultProps = {
  unassembledGroups: [],
  assembledGroups: [],
  token: ''
}

const mapStateToProps = createStructuredSelector({
  unassembledGroups: makeSelectUnassembledGroups(),
  assembledGroups: makeSelectAssembledGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getUnassembledGroups: dispatch(getUnassembledGroups()),
    getAssembledGroups: dispatch(getAssembledGroups())
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'homePage', reducer });
const withSaga = injectSaga({ key: 'homePage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(HomePage);
