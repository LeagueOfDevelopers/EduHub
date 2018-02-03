/**
 *
 * GroupsPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from './reducer';
import saga from './saga';
import {Link} from "react-router-dom";
import {getUnassembledGroups} from "./actions";
import {makeSelectUnassembledGroups} from "./selectors";
import {Row, Col, Menu, Dropdown, Icon} from 'antd';
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";

export class GroupsPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      title: 'Группы',
      sortTitle: 'Сортировка',
      arrow: 'down'
    }
  }


  sortMenu = (
    <Menu>
      <Menu.Item key="0">
        <div onClick={() => this.setState({sortTitle: 'По возрастанию цены', arrow: 'up'})}>По возрастанию цены</div>
      </Menu.Item>
      <Menu.Item key="1">
        <div onClick={() => this.setState({sortTitle: 'По убыванию цены', arrow: 'down'})}>По убыванию цены</div>
      </Menu.Item>
    </Menu>
  );

  componentDidMount() {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.getUnassembledGroups(); // Получение групп, надо будет передавать тип групп
      if(this.props.match.params.groupsTitle === 'unassembledGroups') {
        this.setState({title: 'Незаполненные группы'});
      }
      else if(this.props.match.params.groupsTitle === 'assembledGroups') {
        this.setState({title: 'Идет набор'});
      }
    }
  }

  render() {
    return (
      <Col span={20} offset={2} style={{marginTop: 40}}>
        <Row type='flex' justify='space-between' align='middle'>
          <Col span={12} style={{fontSize: 24}}>{this.state.title}</Col>
          <Col span={12} style={{textAlign: 'right', fontSize: 14}}>
            <Dropdown overlay={this.sortMenu} trigger={['click']} placement='bottomRight'>
              <span>
                {this.state.sortTitle} {this.state.arrow === 'down' ?
                (<Icon type="down"/>)
                :
                (<Icon type="up" style={{fontSize: 10}}/>)}
              </span>
            </Dropdown>
          </Col>
        </Row>
        <Row className='cards-holder cards-holder-center font-size-20' style={{marginTop: 60, marginBottom: 160}}>
          {this.props.groups.map((item) =>
            <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
              <UnassembledGroupCard {...item}/>
            </Link>
          )}
        </Row>
      </Col>
    );
  }
}

GroupsPage.defaultProps = {
  title: 'Группы'
};

GroupsPage.propTypes = {
  dispatch: PropTypes.func,
};

const mapStateToProps = createStructuredSelector({
  groups: makeSelectUnassembledGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getUnassembledGroups:() => dispatch(getUnassembledGroups()),
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'groupsPage', reducer });
const withSaga = injectSaga({ key: 'groupsPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(GroupsPage);
