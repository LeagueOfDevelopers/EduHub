import { createSelector } from 'reselect';

/**
 * Direct selector to the HomePage state domain
 */
const selectHomePage = (state) => state.get('homePage');

/**
 * Other specific selectors
 */

const makeSelectUnassembledGroups = () => createSelector(
  selectHomePage,
  (homeState) => homeState.get('unassembledGroups')
);

const makeSelectAssembledGroups = () => createSelector(
  selectHomePage,
  (homeState) => homeState.get('assembledGroups')
);

/**
 * Default selector used by HomePage
 */

export {
  selectHomePage,
  makeSelectUnassembledGroups,
  makeSelectAssembledGroups
};
