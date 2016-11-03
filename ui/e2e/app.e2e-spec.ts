import { SkillsPage } from './app.po';

describe('skills App', function() {
  let page: SkillsPage;

  beforeEach(() => {
    page = new SkillsPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
