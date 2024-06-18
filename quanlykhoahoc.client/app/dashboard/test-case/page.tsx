"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { TestCaseClient, TestCaseMapping } from "../../web-api-client";

export default function DashboardTestCase() {
  const TestCaseService = new TestCaseClient();

  return (
    <DataTable
      url="/test-case"
      fields={Object.keys(new TestCaseMapping().toJSON())}
      deleteAction={(id) => TestCaseService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        TestCaseService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
