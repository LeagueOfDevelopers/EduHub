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
import {Card, Col, Row, Button} from 'antd';
import styled from 'styled-components';

import Header from 'components/Header';
import UnassembledGroupCard from 'components/UnassembledGroupCard';
import AssembledGroupCard from 'components/AssembledGroupCard';
import {Link} from "react-router-dom";
import { getAssembledGroups, getUnassembledGroups } from "./actions";

// const unassembledGroups = [
//   {
//     title: 'Группа 1',
//     link: '#'
//   },
//   {
//     title: 'Группа 2',
//     link: '#'
//   },
//   {
//     title: 'Группа 3',
//     link: '#'
//   },
//   {
//     title: 'Группа 4',
//     link: '#'
//   },
//   {
//     title: 'Группа 5',
//     link: '#'
//   },
//   {
//     title: 'Группа 6',
//     link: '#'
//   },
//   {
//     title: 'Группа 7',
//     link: '#'
//   },
//   {
//     title: 'Группа 8',
//     link: '#'
//   }
// ]
//
// const assembledGroups = [
//   {
//     title: 'Группа 1',
//     link: '#'
//   },
//   {
//     title: 'Группа 2',
//     link: '#'
//   },
//   {
//     title: 'Группа 3',
//     link: '#'
//   },
//   {
//     title: 'Группа 4',
//     link: '#'
//   },
//   {
//     title: 'Группа 5',
//     link: '#'
//   },
//   {
//     title: 'Группа 6',
//     link: '#'
//   },
//   {
//     title: 'Группа 7',
//     link: '#'
//   },
//   {
//     title: 'Группа 8',
//     link: '#'
//   }
// ]

export class HomePage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function

  componentDidMount() {
    this.props.getUnassembledGroups();
    // this.props.getAssembledProps();
  }

  render() {
    return (
      <div>
        <header>
          <Header/>
        </header>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Незаполненные группы'
            bordered={false}
            className='unassembled-groups-list font-size-20'
            extra={<a href='#'>Показать больше</a>}
          >
            <div className='cards-holder'>
              {this.props.unassembledGroups.map((item,index) =>
                <UnassembledGroupCard {...item}/>
              )}
            </div>
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
            extra={<a href='#'>Показать больше</a>}
          >
            <div className='cards-holder'>
              {this.props.unassembledGroups.map((item,index) =>
                <AssembledGroupCard {...item}/>
              )}
            </div>
            <Row type='flex' justify='end' align='middle' style={{marginTop: 30}}>
              <Col style={{fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
              <Button type="primary" htmlType="submit">Стать преподавателем</Button>
            </Row>
          </Card>
        </Col>
      </div>
    );
  }
}

HomePage.propTypes = {
  dispatch: PropTypes.func.isRequired,
};

HomePage.defaultProps = {
  unassembledGroups: [],
  // assembledGroups: []
}

const mapStateToProps = createStructuredSelector({
  //homepage: makeSelectHomePage(),
  unassembledGroups: makeSelectUnassembledGroups(),
  // assembledGroups: makeSelectAssembledGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getUnassembledGroups: dispatch(getUnassembledGroups()),
    // getAssembledGroups: dispatch(getAssembledGroups())
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
