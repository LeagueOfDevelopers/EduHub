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
import {Card, Col, Row, Button} from 'antd';
import styled from 'styled-components';

import Header from 'components/Header';
import UnassembledGroupCard from 'components/UnassembledGroupCard';
import AssembledGroupCard from 'components/AssembledGroupCard';
import {Link} from "react-router-dom";

const unassembledGroups = [
  {
    title: 'Группа 1',
    link: '#'
  },
  {
    title: 'Группа 2',
    link: '#'
  },
  {
    title: 'Группа 3',
    link: '#'
  },
  {
    title: 'Группа 4',
    link: '#'
  },
  {
    title: 'Группа 5',
    link: '#'
  },
  {
    title: 'Группа 6',
    link: '#'
  },
  {
    title: 'Группа 7',
    link: '#'
  },
  {
    title: 'Группа 8',
    link: '#'
  }
]

const assembledGroups = [
  {
    title: 'Группа 1',
    link: '#'
  },
  {
    title: 'Группа 2',
    link: '#'
  },
  {
    title: 'Группа 3',
    link: '#'
  },
  {
    title: 'Группа 4',
    link: '#'
  },
  {
    title: 'Группа 5',
    link: '#'
  },
  {
    title: 'Группа 6',
    link: '#'
  },
  {
    title: 'Группа 7',
    link: '#'
  },
  {
    title: 'Группа 8',
    link: '#'
  }
]

export default class HomePage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
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
            className='unassembled-groups-list'
            extra={<a href='#'>Показать больше</a>}
          >
            <div className='cards-holder'>
              {unassembledGroups.map((item,index) =>
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
            className='assembled-groups-list'
            extra={<a href='#'>Показать больше</a>}
          >
            <div className='cards-holder'>
              {assembledGroups.map((item,index) =>
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
