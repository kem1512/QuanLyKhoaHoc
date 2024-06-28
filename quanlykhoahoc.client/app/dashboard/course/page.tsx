"use client";

import { CourseClient, CourseMapping } from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardCourse() {
  const CourseService = new CourseClient();

  return (
    <DataTable
      url="/course"
      fields={Object.keys(new CourseMapping().toJSON()).filter(
        (c) => c !== "courseSubjects" && c !== "creator"
      )}
      deleteAction={(id) => CourseService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        CourseService.getEntities(null, filters, sorts, page, pageSize)
      }
    />
  );
}
