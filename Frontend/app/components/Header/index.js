/**
*
* Header
*
*/

import React from 'react';
import styled from 'styled-components';

const Wrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 2rem 6rem;
  background-color: #c4c4c4;
  height: 6.5rem;
`

const HeaderRight = Wrapper.extend`
  width: 60%;
  padding: 0;
  margin-left: 40%;
`

const Logo = styled.div`
  font-size: 3.6rem;
`

const Search = styled.div`
  display: flex;
  align-items: center;
  height: 4rem;
  width: 60%;
  min-width: 180px;
  margin-right: 10%;
  padding: 0 2rem;
  background-color:#b2b2b2;
  border-radius: 0.4rem;
  
`

const SearchForm = styled.input`
  width: 100%;
  
  &:focus {
    outline: none;
  }
`

const SearchBtn = styled.div`
  height: 2.4rem;
  width: 2.4rem;
  margin-left: 4%;
  background-image:url(${require('images/search.svg')});
  
  &:hover {
    cursor: pointer;
  }
`

const Profile = styled.div`
  display: flex;
  align-items: center;
`

const UserIcon = styled.div`
  width: 4.2rem;
  height: 4.2rem;
  background-color:#fff;
  border-radius: 50%;
  cursor: pointer;
`

const UserName = styled.div`
  margin-left: 1rem;
  cursor: pointer;
`

class Header extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Wrapper>
        <Logo>Logo</Logo>
        <HeaderRight>
          <Search>
            <SearchForm type='search' placeholder='Поиск'></SearchForm>
            <SearchBtn/>
          </Search>
          <Profile>
            <UserIcon/>
            <UserName>Имя Фамилия</UserName>
          </Profile>
        </HeaderRight>
      </Wrapper>
    );
  }
}

Header.propTypes = {

};

export default Header;
