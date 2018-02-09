import { createSelector } from 'reselect';

/**
 * Direct selector to the HomePage state domain
 */
const selectHomePage = (state) => state.get('homePage');

/**
 * Other specific selectors
 */

const makeSelectGroups = (groupsType) => createSelector(
  selectHomePage,
  (homeState) => homeState.get(groupsType)
);

/**
 * Default selector used by HomePage
 */

export {
  selectHomePage,
  makeSelectGroups,
};
