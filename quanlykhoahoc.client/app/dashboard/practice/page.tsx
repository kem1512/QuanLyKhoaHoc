"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { PracticeClient, PracticeMapping } from "../../web-api-client";

export default function DashboardPractice() {
  const PracticeService = new PracticeClient();

  return (
    <DataTable
      url="/practice"
      fields={Object.keys(new PracticeMapping().toJSON()).filter(
        (c) => c !== "subjectDetail" && c !== "programingLanguage"
      )}
      deleteAction={(id) => PracticeService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        PracticeService.getEntities(null, filters, sorts, page, pageSize)
      }
    />
  );
}
