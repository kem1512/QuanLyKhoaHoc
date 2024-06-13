"use client";

import TestCaseHandler from "../../../../components/Handler/TestCaseHandler";

export default function DashboardTestCaseUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <TestCaseHandler id={id} />;
}
