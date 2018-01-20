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
import { selectHomePage,
         makeSelectAssembledGroups,
         makeSelectUnassembledGroups
} from "./selectors";
import reducer from './reducer';
import saga from './saga';

import {Card, Col, Row, Button, message} from 'antd';
// import styled from 'styled-components';

import UnassembledGroupCard from 'components/UnassembledGroupCard';
import AssembledGroupCard from 'components/AssembledGroupCard';
import SigningInForm from 'containers/SigningInForm';
import {Link} from "react-router-dom";
import { getAssembledGroups, getUnassembledGroups } from "./actions";

import  config from '../../config';


const unassembledGroups = [
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  },
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  },
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  },
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  }
];

const assembledGroups = [
  {
    groupInfo: {
      id: 2,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf'],
      description: 'dadasddas'
    }
  }
];

export class HomePage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.makeTeacher = this.makeTeacher.bind(this);
    this.handleCancel = this.handleCancel.bind(this);

    this.state = {
      signInVisible: false,
      user: 'qwerty'
    }
  }

  componentDidMount() {
    if(!config.USE_GAGS) {
      this.props.getUnassembledGroups();
      this.props.getAssembledGroups();
    }
  }

  makeTeacher() {
    if(localStorage.getItem('token'))
      message.success('Теперь вы можете преподавать!');
    else {
      this.setState({signInVisible: true})
  }}

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  unassembledGroups = (
    <div className='cards-holder'>
      {this.props.unassembledGroups.map((item, i) =>
        <Link to={`/group/${item.groupInfo.id}`}>
          <UnassembledGroupCard {...item}/>
        </Link>
      )}
    </div>
  );

  assembledGroups = (
    <div className='cards-holder'>
      {this.props.assembledGroups.map((item, i) =>
        <Link to={`/group/${item.groupInfo.id}`}>
          <AssembledGroupCard {...item}/>
        </Link>
      )}
    </div>
  );

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Незаполненные группы'
            bordered={false}
            className='unassembled-groups-list font-size-20'
            extra={<Link to='#' >Показать больше</Link>}
          >
            {config.USE_GAGS ?
              (
                <div className='cards-holder'>
                  {unassembledGroups.map((item, i) =>
                    <Link to={`/group/${item.groupInfo.id}`}>
                      <UnassembledGroupCard {...item}/>
                    </Link>
                  )}
                </div>
              ) :
              this.unassembledGroups
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
            {config.USE_GAGS > 0 ?
              (
                <div className='cards-holder'>
                  {assembledGroups.map((item, i) =>
                    <Link to={`/group/${item.groupInfo.id}`}>
                      <AssembledGroupCard {...item}/>
                    </Link>
                  )}
                </div>
              ) :
              this.assembledGroups
            }
            <Row type='flex' justify='end' align='middle' style={{marginTop: 30}}>
              <Col style={{fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
              <Button type="primary" onClick={this.makeTeacher}>Стать преподавателем</Button>
              <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
            </Row>
          </Card>
        </Col>
      </div>
    );
  }
}

HomePage.propTypes = {
  makeTeacher: PropTypes.func,
  unassembledGroups: PropTypes.oneOfType([
    PropTypes.array,
    PropTypes.object
  ]),
  assembledGroups: PropTypes.oneOfType([
    PropTypes.array,
    PropTypes.object
  ])
};

const mapStateToProps = createStructuredSelector({
  unassembledGroups: makeSelectUnassembledGroups(),
  assembledGroups: makeSelectAssembledGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getUnassembledGroups: () => dispatch(getUnassembledGroups()),
    getAssembledGroups: () => dispatch(getAssembledGroups())
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
